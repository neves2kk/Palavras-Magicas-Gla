using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;



public class AuthManager : MonoBehaviour
{

    public static AuthManager UIManagerInstance;
    public GameObject MenuPrincipal;
    public GameObject loginUI;
    public GlBoardController glboard;

    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    //Login variables
    [Header("Login")]
    public InputField emailLoginField;
    public InputField passwordLoginField;

    //Register variables
    [Header("Register")]
    public InputField usernameRegisterField;
    public InputField emailRegisterField;
    public InputField passwordRegisterField;
    public InputField birthdayRegisterField;
    public InputField genderRegisterField;

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        glboard = GameObject.FindGameObjectWithTag("GLBOARD").GetComponent<GlBoardController>();
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (PlayerPrefs.GetInt("logado") == 1)
            {
                glboard.instantiateGlBoard(PlayerPrefs.GetString("id"));
                glboard.setLastLogin();
            }
            
                
        }
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            Debug.Log(message);
        }
        else
        {
            User = LoginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            MenuPrincipal.SetActive(true);
            loginUI.SetActive(false);
            PlayerPrefs.SetInt("logado", 1);
            PlayerPrefs.SetString("id", User.UserId);
            glboard.instantiateGlBoard(User.UserId);
            glboard.setLastLogin();
            
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {

        Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
        yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);
        if (RegisterTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
            FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Register Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WeakPassword:
                    message = "Weak Password";
                    break;
                case AuthError.EmailAlreadyInUse:
                    message = "Email Already In Use";
                    break;
            }
            Debug.Log(message);
        }
        else
        {
            User = RegisterTask.Result.User;

            if (User != null)
            {
                //Create a user profile and set the username
                UserProfile profile = new UserProfile { DisplayName = _username };

                //Call the Firebase auth update user profile function passing the profile with the username
                Task ProfileTask = User.UpdateUserProfileAsync(profile);
                //Wait until the task completes
                yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                if (ProfileTask.Exception != null)
                {
                    //If there are errors handle them
                    Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                    FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                }
                else
                {
                    //Username is now set
                    //Now return to login screen
                    UiManagerController.instance.loginScreen();
                    GlBoardController glBoard = GameObject.FindGameObjectWithTag("GLBOARD").GetComponent<GlBoardController>();
                    glBoard.instantiateGlBoard(User.UserId);
                    glBoard.setUserData(usernameRegisterField.text, birthdayRegisterField.text, genderRegisterField.text);
                }
            }
        }
    }
}