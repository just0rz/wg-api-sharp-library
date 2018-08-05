
using System;

namespace WGSharpAPITests
{
    public static class TestConstants
    {
        public static class Category
        {
            public const string Integration = "Integration tests";
            public const string Dev = "Unit tests";
        }

        public static class Just0rzAccount
        {
            // My account details :) - you may modify this
            public const long AccountId = 508637087; // account id
            public const long CreatedAt = 1361472166; // unix timestamp
            public const long GrilleTankId = 5649; // Grille tank id
            public const long GrilleEngineId = 13333; // Grille engine module id
            public const long GrilleRadioId = 2071; // Grille radio module id
            public const long GrilleSuspensionId = 11538; // Grille suspension module id
            public const long GrilleGunId = 1556; // Grille gun module id
            public const long T54TurretId = 14595; // T-54 turret module id
            public const string Username = "Just0rz"; // my account username
        }

        public static class Application
        {
            // you may change this, but don't make it public
            public const string ID = "demo";
        }

        public static class JsonResponse
        {
            public static string MockDataRoot = $"{Environment.CurrentDirectory}\\MockData\\";
            public static string AccountsRoot = $"{MockDataRoot}Accounts\\";

            public static string SearchPlayerResult_1_valid = $"{AccountsRoot}SearchPlayer_1_valid.json";
            public static string SearchPlayer_1_exact_result = $"{AccountsRoot}SearchPlayer_1_exact_result.json";
            public static string SearchPlayer_1_account_id_result_only = $"{AccountsRoot}SearchPlayer_1_account_id_result_only.json"; 
            public static string SearchPlayer_invalid_2_letters_used = $"{AccountsRoot}SearchPlayer_invalid_2_letters_used.json";
            public static string PlayerData_NoPrivate = $"{AccountsRoot}PlayerData_NoPrivate.json";
            public static string PlayerData_Vehicles = $"{AccountsRoot}PlayerData_Vehicles.json";
            public static string PlayerData_Achievements = $"{AccountsRoot}PlayerData_Achievements.json";
        }
    }
}
