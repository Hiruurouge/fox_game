using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public static class PlayfabAuth
{
    // Const - Save email/password
    public const string PlayfabAuthPlayerPrefsKeyUsername = "playfab_auth_username";
    public const string PlayfabAuthPlayerPrefsKeyEmail = "playfab_auth_email";
    public const string PlayfabAuthPlayerPrefsKeyPassword = "playfab_auth_password";
    //public static Text messageText;
    // Getter
    public static bool IsLoggedIn
    {
        get
        {
            // TODO: Implement check that we are logged in
            return PlayFabClientAPI.IsClientLoggedIn();
        }
    }
    // Functions
   /* public static void TryRegisterWithEmail(string email, string password, Action registerResultCallback, Action errorCallback)
    {
        PlayfabAuth.TryRegisterWithEmail(email, password, email, registerResultCallback, errorCallback);
    }*/

    public static void TryRegisterWithEmail(string email, string password, string username, Action<RegisterPlayFabUserResult> registerResultCallback, Action<PlayFabError> errorCallback)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email,
            Password = password,
            Username = username,
            RequireBothUsernameAndEmail = true
        };
        PlayFabClientAPI.RegisterPlayFabUser(request,registerResultCallback,errorCallback);

    }
    public static void TryLoginWithEmail(string email, string password, Action<LoginResult> loginResultCallback, Action<PlayFabError> errorCallback)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };
        PlayFabClientAPI.LoginWithEmailAddress(request,loginResultCallback,errorCallback);
        //loginResultCallback.Invoke();
    }

    // Logout
    public static void Logout(Action logoutResultCallback, Action errorCallback)
    {
        // Clear all keys from PlayerPrefs
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyUsername);
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyEmail);
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyPassword);

        // Callback
        logoutResultCallback.Invoke();
    }
}
