using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSystem : MonoBehaviour
{
    private static string CURRENCY_EN = "$";
    private static string CURRENCY_DE = "€";
    private static string CURRENCY_TR = "₺";
    
    private static string INFINITY_EN = "∞";
    private static string INFINITY_DE = "∞";
    private static string INFINITY_TR = "∞";
    
    private static string NAME_EN = "Name";
    private static string NAME_DE = "Name";
    private static string NAME_TR = "";
    
    // Setting Names
    private static string PLAYER_STATUS_NO_NAME_ME_EN = "Set your Name";
    private static string PLAYER_STATUS_NO_NAME_ME_DE = "";
    private static string PLAYER_STATUS_NO_NAME_ME_TR = "Set your Name";
    
    private static string PLAYER_STATUS_NO_NAME_TEAM_EN = "Set his Name";
    private static string PLAYER_STATUS_NO_NAME_TEAM_DE = "";
    private static string PLAYER_STATUS_NO_NAME_TEAM_TR = "";
    
    // Main Menu Buttons
    private static string RANKED_BUTTON_LABEL_EN = "Ranked";
    private static string RANKED_BUTTON_LABEL_DE = "";
    private static string RANKED_BUTTON_LABEL_TR = "";
    
    private static string RANKED_OFFLINE_BUTTON_LABEL_EN = "Ranked Offline";
    private static string RANKED_OFFLINE_BUTTON_LABEL_DE = "";
    private static string RANKED_OFFLINE_BUTTON_LABEL_TR = "";
    
    private static string TEAM_EDITOR_BUTTON_LABEL_EN = "Team Editor";
    private static string TEAM_EDITOR_BUTTON_LABEL_DE = "";
    private static string TEAM_EDITOR_BUTTON_LABEL_TR = "";
    
    private static string INVENTORY_BUTTON_LABEL_EN = "Inventory";
    private static string INVENTORY_BUTTON_LABEL_DE = "";
    private static string INVENTORY_BUTTON_LABEL_TR = "";
    
    private static string REMOVE_ADS_BUTTON_LABEL_EN = "Remove Ads";
    private static string REMOVE_ADS_BUTTON_LABEL_DE = "";
    private static string REMOVE_ADS_BUTTON_LABEL_TR = "";
    
    private static string MORE_COIN_BUTTON_LABEL_EN = "More Coins";
    private static string MORE_COIN_BUTTON_LABEL_DE = "";
    private static string MORE_COIN_BUTTON_LABEL_TR = "";
    
    private static string RESTORE_PURCHASE_BUTTON_LABEL_EN = "Restore Purchase";
    private static string RESTORE_PURCHASE_BUTTON_LABEL_DE = "";
    private static string RESTORE_PURCHASE_BUTTON_LABEL_TR = "";
    
    // Other Menu Buttons
    private static string START_SEARCH_BUTTON_LABEL_EN = "START SEARCH";
    private static string START_SEARCH_BUTTON_LABEL_DE = "";
    private static string START_SEARCH_BUTTON_LABEL_TR = "";
    
    private static string CANCEL_SEARCH_BUTTON_LABEL_EN = "CANCEL SEARCH";
    private static string CANCEL_SEARCH_BUTTON_LABEL_DE = "";
    private static string CANCEL_SEARCH_BUTTON_LABEL_TR = "";
    
    private static string STORE_TAB_BUTTON_LABEL_EN = "Store";
    private static string STORE_TAB_BUTTON_LABEL_DE = "";
    private static string STORE_TAB_BUTTON_LABEL_TR = "";
    
    private static string INVENTORY_TAB_BUTTON_LABEL_EN = "Inventory";
    private static string INVENTORY_TAB_BUTTON_LABEL_DE = "";
    private static string INVENTORY_TAB_BUTTON_LABEL_TR = "";
    
    private static string STORE_BUY_BUTTON_LABEL_EN = "Buy";
    private static string STORE_BUY_BUTTON_LABEL_DE = "";
    private static string STORE_BUY_BUTTON_LABEL_TR = "";
    
    private static string STORE_Bought_BUTTON_LABEL_EN = "Bought";
    private static string STORE_Bought_BUTTON_LABEL_DE = "";
    private static string STORE_Bought_BUTTON_LABEL_TR = "";
    
    // Popup Menus & Buttons
    private static string EQUIP_MENU_TITLE_EN = "Equip Menu";
    private static string EQUIP_MENU_TITLE_DE = "";
    private static string EQUIP_MENU_TITLE_TR = "";
    
    private static string EQUIP_MENU_BUTTON_LABEL_T_EN = "T";
    private static string EQUIP_MENU_BUTTON_LABEL_T_DE = "T";
    private static string EQUIP_MENU_BUTTON_LABEL_T_TR = "T";
    
    private static string EQUIP_MENU_BUTTON_LABEL_CT_EN = "CT";
    private static string EQUIP_MENU_BUTTON_LABEL_CT_DE = "CT";
    private static string EQUIP_MENU_BUTTON_LABEL_CT_TR = "CT";
    
    private static string EQUIP_MENU_BUTTON_LABEL_BOTH_EN = "T & CT";
    private static string EQUIP_MENU_BUTTON_LABEL_BOTH_DE = "T & CT";
    private static string EQUIP_MENU_BUTTON_LABEL_BOTH_TR = "T & CT";
    
    private static string EQUIP_MENU_BUTTON_LABEL_CANCEL_EN = "Cancel";
    private static string EQUIP_MENU_BUTTON_LABEL_CANCEL_DE = "";
    private static string EQUIP_MENU_BUTTON_LABEL_CANCEL_TR = "";
    

    private static string EXIT_GAME_MENU_TITLE_EN = "Are you sure you want to exit?";
    private static string EXIT_GAME_MENU_TITLE_DE = "";
    private static string EXIT_GAME_MENU_TITLE_TR = "";
    
    private static string EXIT_GAME_MENU_BUTTON_LABEL_YES_EN = "Yes";
    private static string EXIT_GAME_MENU_BUTTON_LABEL_YES_DE = "";
    private static string EXIT_GAME_MENU_BUTTON_LABEL_YES_TR = "";
    
    private static string EXIT_GAME_MENU_BUTTON_LABEL_NO_EN = "No";
    private static string EXIT_GAME_MENU_BUTTON_LABEL_NO_DE = "";
    private static string EXIT_GAME_MENU_BUTTON_LABEL_NO_TR = "";


    private static string MORE_PLAYS_MENU_TITLE_EN = "Are you sure you want to exit?";
    private static string MORE_PLAYS_MENU_TITLE_DE = "";
    private static string MORE_PLAYS_MENU_TITLE_TR = "";
    
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_BUY_NO_PLAYS_EN = "Buy perm Plays";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_BUY_NO_PLAYS_DE = "";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_BUY_NO_PLAYS_TR = "";
    
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_BUY_20_PLAYS_EN = "Buy 20 Plays";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_BUY_20_PLAYS_DE = "";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_BUY_20_PLAYS_TR = "";
    
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_WATCH_AD_EN = "Watch ad";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_WATCH_AD_DE = "";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_WATCH_AD_TR = "";
    
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_CLOSE_EN = "Close";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_CLOSE_DE = "";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_CLOSE_TR = "";
    
    
    private static string SEARCH_GAME_MENU_TITLE_EN = "YOUR MATCH IS READY";
    private static string SEARCH_GAME_MENU_TITLE_DE = "";
    private static string SEARCH_GAME_MENU_TITLE_TR = "";
    
    private static string SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPT_EN = "ACCEPT";
    private static string SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPT_DE = "";
    private static string SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPT_TR = "";
    
    private static string SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPTED_EN = "ACCEPTED";
    private static string SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPTED_DE = "";
    private static string SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPTED_TR = "";
    
    
    private static string SET_TEAM_NAMES_MENU_TITLE_EN = "You have to set names for your Team. Ve kendi ismin.";
    private static string SET_TEAM_NAMES_MENU_TITLE_DE = "";
    private static string SET_TEAM_NAMES_MENU_TITLE_TR = "";
    
    private static string SET_TEAM_NAMES_MENU_BUTTON_LABEL_CLOSE_EN = "Close";
    private static string SET_TEAM_NAMES_MENU_BUTTON_LABEL_CLOSE_DE = "";
    private static string SET_TEAM_NAMES_MENU_BUTTON_LABEL_CLOSE_TR = "";
    
    private static string SET_TEAM_NAMES_MENU_BUTTON_LABEL_SET_TEAM_EN = "Set Team";
    private static string SET_TEAM_NAMES_MENU_BUTTON_LABEL_SET_TEAM_DE = "";
    private static string SET_TEAM_NAMES_MENU_BUTTON_LABEL_SET_TEAM_TR = "";
    
    // Game Texts & Buttons
    private static string BUY_PANEL_BUTTON_LABEL_FLASHBANG_EN = "Flashbang";
    private static string BUY_PANEL_BUTTON_LABEL_FLASHBANG_DE = "";
    private static string BUY_PANEL_BUTTON_LABEL_FLASHBANG_TR = "";
    
    private static string BUY_PANEL_BUTTON_LABEL_ZEUS_EN = "Zeus x27";
    private static string BUY_PANEL_BUTTON_LABEL_ZEUS_DE = "";
    private static string BUY_PANEL_BUTTON_LABEL_ZEUS_TR = "";
    
    private static string BUY_PANEL_BUTTON_LABEL_DEFUSE_EN = "Defuse Kit";
    private static string BUY_PANEL_BUTTON_LABEL_DEFUSE_DE = "";
    private static string BUY_PANEL_BUTTON_LABEL_DEFUSE_TR = "";
    
    
    private static string SHOW_WEAPON_PANEL_TITLE_START_EN = "You killed with"; // Mali's
    private static string SHOW_WEAPON_PANEL_TITLE_MIDDLE_EN = "'s";
    private static string SHOW_WEAPON_PANEL_TITLE_END_EN = "weapon";
    
    private static string SHOW_WEAPON_PANEL_TITLE_START_DE = "";
    private static string SHOW_WEAPON_PANEL_TITLE_MIDDLE_DE = "";
    private static string SHOW_WEAPON_PANEL_TITLE_END_DE = "";
    
    private static string SHOW_WEAPON_PANEL_TITLE_START_TR = "";
    private static string SHOW_WEAPON_PANEL_TITLE_MIDDLE_TR = "";
    private static string SHOW_WEAPON_PANEL_TITLE_END_TR = "";

    
    private static string END_GAME_PANEL_LABEL_KILLS_EN = "Kills : ";
    private static string END_GAME_PANEL_LABEL_KILLS_DE = "";
    private static string END_GAME_PANEL_LABEL_KILLS_TR = "";
    
    private static string END_GAME_PANEL_LABEL_WINS_EN = "Wins : ";
    private static string END_GAME_PANEL_LABEL_WINS_DE = "";
    private static string END_GAME_PANEL_LABEL_WINS_TR = "";
    
    private static string END_GAME_PANEL_LABEL_MONEY_EN = "Money : " + GET_CURRENCY();
    private static string END_GAME_PANEL_LABEL_MONEY_DE = "" + GET_CURRENCY();
    private static string END_GAME_PANEL_LABEL_MONEY_TR = "" + GET_CURRENCY();
    
    
    private static string END_ROUND_PANEL_LABEL_T_WIN_EN = "Terrorists Win";
    private static string END_ROUND_PANEL_LABEL_T_WIN_DE = "";
    private static string END_ROUND_PANEL_LABEL_T_WIN_TR = "";
    
    private static string END_ROUND_PANEL_LABEL_CT_WIN_EN = "Anti-Terrorists Win";
    private static string END_ROUND_PANEL_LABEL_CT_WIN_DE = "";
    private static string END_ROUND_PANEL_LABEL_CT_WIN_TR = "";
    
    // Connections
    
    private static string IS_CONNECTING_LABEL_EN = "Connecting";
    private static string IS_CONNECTING_LABEL_DE = "";
    private static string IS_CONNECTING_LABEL_TR = "";
    
    private static string IS_DISCONNECTING_LABEL_EN = "Disconnecting";
    private static string IS_DISCONNECTING_LABEL_DE = "";
    private static string IS_DISCONNECTING_LABEL_TR = "";
    
    private static string IS_SEARCHING_PLAYER_LABEL_EN = "Waiting for player";
    private static string IS_SEARCHING_PLAYER_LABEL_DE = "";
    private static string IS_SEARCHING_PLAYER_LABEL_TR = "";
    
    public static string GET_CURRENCY()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return CURRENCY_DE;
            case SystemLanguage.Turkish:
                return CURRENCY_TR;
        }
        return         CURRENCY_EN;
    }
    
    public static string GET_INFINITY()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return INFINITY_DE;
            case SystemLanguage.Turkish:
                return INFINITY_TR;
        }
        return         INFINITY_EN;
    }
    
    public static string GET_NAME()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return NAME_DE;
            case SystemLanguage.Turkish:
                return NAME_TR;
        }
        return         NAME_EN;
    }

    public static string GET_PLAYER_STATUS_NO_NAME_ME()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return PLAYER_STATUS_NO_NAME_ME_DE;
            case SystemLanguage.Turkish:
                return PLAYER_STATUS_NO_NAME_ME_TR;
        }
        return         PLAYER_STATUS_NO_NAME_ME_EN;
    }

    public static string GET_PLAYER_STATUS_NO_NAME_TEAM()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return PLAYER_STATUS_NO_NAME_TEAM_DE;
            case SystemLanguage.Turkish:
                return PLAYER_STATUS_NO_NAME_TEAM_TR;
        }
        return         PLAYER_STATUS_NO_NAME_TEAM_EN;
    }

    public static string GET_RANKED_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return RANKED_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return RANKED_BUTTON_LABEL_TR;
        }
        return         RANKED_BUTTON_LABEL_EN;
    }

    public static string GET_RANKED_OFFLINE_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return RANKED_OFFLINE_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return RANKED_OFFLINE_BUTTON_LABEL_TR;
        }
        return         RANKED_OFFLINE_BUTTON_LABEL_EN;
    }

    public static string GET_TEAM_EDITOR_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return TEAM_EDITOR_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return TEAM_EDITOR_BUTTON_LABEL_TR;
        }
        return         TEAM_EDITOR_BUTTON_LABEL_EN;
    }

    public static string GET_INVENTORY_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return INVENTORY_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return INVENTORY_BUTTON_LABEL_TR;
        }
        return         INVENTORY_BUTTON_LABEL_EN;
    }

    public static string GET_REMOVE_ADS_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return REMOVE_ADS_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return REMOVE_ADS_BUTTON_LABEL_TR;
        }
        return         REMOVE_ADS_BUTTON_LABEL_EN;
    }

    public static string GET_MORE_COIN_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return MORE_COIN_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return MORE_COIN_BUTTON_LABEL_TR;
        }
        return         MORE_COIN_BUTTON_LABEL_EN;
    }

    public static string GET_RESTORE_PURCHASE_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return RESTORE_PURCHASE_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return RESTORE_PURCHASE_BUTTON_LABEL_TR;
        }
        return         RESTORE_PURCHASE_BUTTON_LABEL_EN;
    }

    public static string GET_START_SEARCH_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return START_SEARCH_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return START_SEARCH_BUTTON_LABEL_TR;
        }
        return         START_SEARCH_BUTTON_LABEL_EN;
    }

    public static string GET_CANCEL_SEARCH_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return CANCEL_SEARCH_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return CANCEL_SEARCH_BUTTON_LABEL_TR;
        }
        return         CANCEL_SEARCH_BUTTON_LABEL_EN;
    }

    public static string GET_STORE_TAB_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return STORE_TAB_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return STORE_TAB_BUTTON_LABEL_TR;
        }
        return         STORE_TAB_BUTTON_LABEL_EN;
    }

    public static string GET_INVENTORY_TAB_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return INVENTORY_TAB_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return INVENTORY_TAB_BUTTON_LABEL_TR;
        }
        return         INVENTORY_TAB_BUTTON_LABEL_EN;
    }

    public static string GET_STORE_BUY_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return STORE_BUY_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return STORE_BUY_BUTTON_LABEL_TR;
        }
        return         STORE_BUY_BUTTON_LABEL_EN;
    }

    public static string GET_STORE_Bought_BUTTON_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return STORE_Bought_BUTTON_LABEL_DE;
            case SystemLanguage.Turkish:
                return STORE_Bought_BUTTON_LABEL_TR;
        }
        return         STORE_Bought_BUTTON_LABEL_EN;
    }

    public static string GET_EQUIP_MENU_TITLE()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return EQUIP_MENU_TITLE_DE;
            case SystemLanguage.Turkish:
                return EQUIP_MENU_TITLE_TR;
        }
        return         EQUIP_MENU_TITLE_EN;
    }

    public static string GET_EQUIP_MENU_BUTTON_LABEL_T()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return EQUIP_MENU_BUTTON_LABEL_T_DE;
            case SystemLanguage.Turkish:
                return EQUIP_MENU_BUTTON_LABEL_T_TR;
        }
        return         EQUIP_MENU_BUTTON_LABEL_T_EN;
    }

    public static string GET_EQUIP_MENU_BUTTON_LABEL_CT()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return EQUIP_MENU_BUTTON_LABEL_CT_DE;
            case SystemLanguage.Turkish:
                return EQUIP_MENU_BUTTON_LABEL_CT_TR;
        }
        return         EQUIP_MENU_BUTTON_LABEL_CT_EN;
    }

    public static string GET_EQUIP_MENU_BUTTON_LABEL_BOTH()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return EQUIP_MENU_BUTTON_LABEL_BOTH_DE;
            case SystemLanguage.Turkish:
                return EQUIP_MENU_BUTTON_LABEL_BOTH_TR;
        }
        return         EQUIP_MENU_BUTTON_LABEL_BOTH_EN;
    }

    public static string GET_EQUIP_MENU_BUTTON_LABEL_CANCEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return EQUIP_MENU_BUTTON_LABEL_CANCEL_DE;
            case SystemLanguage.Turkish:
                return EQUIP_MENU_BUTTON_LABEL_CANCEL_TR;
        }
        return         EQUIP_MENU_BUTTON_LABEL_CANCEL_EN;
    }

    public static string GET_EXIT_GAME_MENU_TITLE()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return EXIT_GAME_MENU_TITLE_DE;
            case SystemLanguage.Turkish:
                return EXIT_GAME_MENU_TITLE_TR;
        }
        return         EXIT_GAME_MENU_TITLE_EN;
    }

    public static string GET_EXIT_GAME_MENU_BUTTON_LABEL_YES()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return EXIT_GAME_MENU_BUTTON_LABEL_YES_DE;
            case SystemLanguage.Turkish:
                return EXIT_GAME_MENU_BUTTON_LABEL_YES_TR;
        }
        return         EXIT_GAME_MENU_BUTTON_LABEL_YES_EN;
    }

    public static string GET_EXIT_GAME_MENU_BUTTON_LABEL_NO()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return EXIT_GAME_MENU_BUTTON_LABEL_NO_DE;
            case SystemLanguage.Turkish:
                return EXIT_GAME_MENU_BUTTON_LABEL_NO_TR;
        }
        return         EXIT_GAME_MENU_BUTTON_LABEL_NO_EN;
    }

    public static string GET_MORE_PLAYS_MENU_TITLE()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return MORE_PLAYS_MENU_TITLE_DE;
            case SystemLanguage.Turkish:
                return MORE_PLAYS_MENU_TITLE_TR;
        }
        return         MORE_PLAYS_MENU_TITLE_EN;
    }

    public static string GET_MORE_PLAYS_MENU_BUTTON_LABEL_BUY_NO_PLAYS()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return MORE_PLAYS_MENU_BUTTON_LABEL_BUY_NO_PLAYS_DE;
            case SystemLanguage.Turkish:
                return MORE_PLAYS_MENU_BUTTON_LABEL_BUY_NO_PLAYS_TR;
        }
        return         MORE_PLAYS_MENU_BUTTON_LABEL_BUY_NO_PLAYS_EN;
    }

    public static string GET_MORE_PLAYS_MENU_BUTTON_LABEL_BUY_20_PLAYS()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return MORE_PLAYS_MENU_BUTTON_LABEL_BUY_20_PLAYS_DE;
            case SystemLanguage.Turkish:
                return MORE_PLAYS_MENU_BUTTON_LABEL_BUY_20_PLAYS_TR;
        }
        return         MORE_PLAYS_MENU_BUTTON_LABEL_BUY_20_PLAYS_EN;
    }

    public static string GET_MORE_PLAYS_MENU_BUTTON_LABEL_WATCH_AD()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return MORE_PLAYS_MENU_BUTTON_LABEL_WATCH_AD_DE;
            case SystemLanguage.Turkish:
                return MORE_PLAYS_MENU_BUTTON_LABEL_WATCH_AD_TR;
        }
        return         MORE_PLAYS_MENU_BUTTON_LABEL_WATCH_AD_EN;
    }

    public static string GET_MORE_PLAYS_MENU_BUTTON_LABEL_CLOSE()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return MORE_PLAYS_MENU_BUTTON_LABEL_CLOSE_DE;
            case SystemLanguage.Turkish:
                return MORE_PLAYS_MENU_BUTTON_LABEL_CLOSE_TR;
        }
        return         MORE_PLAYS_MENU_BUTTON_LABEL_CLOSE_EN;
    }

    public static string GET_SEARCH_GAME_MENU_TITLE()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return SEARCH_GAME_MENU_TITLE_DE;
            case SystemLanguage.Turkish:
                return SEARCH_GAME_MENU_TITLE_TR;
        }
        return         SEARCH_GAME_MENU_TITLE_EN;
    }

    public static string GET_SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPT()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPT_DE;
            case SystemLanguage.Turkish:
                return SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPT_TR;
        }
        return         SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPT_EN;
    }

    public static string GET_SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPTED()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPTED_DE;
            case SystemLanguage.Turkish:
                return SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPTED_TR;
        }
        return         SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPTED_EN;
    }

    public static string GET_SET_TEAM_NAMES_MENU_TITLE()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return SET_TEAM_NAMES_MENU_TITLE_DE;
            case SystemLanguage.Turkish:
                return SET_TEAM_NAMES_MENU_TITLE_TR;
        }
        return         SET_TEAM_NAMES_MENU_TITLE_EN;
    }

    public static string GET_SET_TEAM_NAMES_MENU_BUTTON_LABEL_CLOSE()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return SET_TEAM_NAMES_MENU_BUTTON_LABEL_CLOSE_DE;
            case SystemLanguage.Turkish:
                return SET_TEAM_NAMES_MENU_BUTTON_LABEL_CLOSE_TR;
        }
        return         SET_TEAM_NAMES_MENU_BUTTON_LABEL_CLOSE_EN;
    }

    public static string GET_SET_TEAM_NAMES_MENU_BUTTON_LABEL_SET_TEAM()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return SET_TEAM_NAMES_MENU_BUTTON_LABEL_SET_TEAM_DE;
            case SystemLanguage.Turkish:
                return SET_TEAM_NAMES_MENU_BUTTON_LABEL_SET_TEAM_TR;
        }
        return         SET_TEAM_NAMES_MENU_BUTTON_LABEL_SET_TEAM_EN;
    }

    public static string GET_BUY_PANEL_BUTTON_LABEL_FLASHBANG()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return BUY_PANEL_BUTTON_LABEL_FLASHBANG_DE;
            case SystemLanguage.Turkish:
                return BUY_PANEL_BUTTON_LABEL_FLASHBANG_TR;
        }
        return         BUY_PANEL_BUTTON_LABEL_FLASHBANG_EN;
    }

    public static string GET_BUY_PANEL_BUTTON_LABEL_ZEUS()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return BUY_PANEL_BUTTON_LABEL_ZEUS_DE;
            case SystemLanguage.Turkish:
                return BUY_PANEL_BUTTON_LABEL_ZEUS_TR;
        }
        return         BUY_PANEL_BUTTON_LABEL_ZEUS_EN;
    }

    public static string GET_BUY_PANEL_BUTTON_LABEL_DEFUSE()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return BUY_PANEL_BUTTON_LABEL_DEFUSE_DE;
            case SystemLanguage.Turkish:
                return BUY_PANEL_BUTTON_LABEL_DEFUSE_TR;
        }
        return         BUY_PANEL_BUTTON_LABEL_DEFUSE_EN;
    }

    public static string GET_SHOW_WEAPON_PANEL_TITLE_START()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return SHOW_WEAPON_PANEL_TITLE_START_DE;
            case SystemLanguage.Turkish:
                return SHOW_WEAPON_PANEL_TITLE_START_TR;
        }
        return         SHOW_WEAPON_PANEL_TITLE_START_EN;
    }

    public static string GET_SHOW_WEAPON_PANEL_TITLE_MIDDLE()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return SHOW_WEAPON_PANEL_TITLE_MIDDLE_DE;
            case SystemLanguage.Turkish:
                return SHOW_WEAPON_PANEL_TITLE_MIDDLE_TR;
        }
        return         SHOW_WEAPON_PANEL_TITLE_MIDDLE_EN;
    }

    public static string GET_SHOW_WEAPON_PANEL_TITLE_END()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return SHOW_WEAPON_PANEL_TITLE_END_DE;
            case SystemLanguage.Turkish:
                return SHOW_WEAPON_PANEL_TITLE_END_TR;
        }
        return         SHOW_WEAPON_PANEL_TITLE_END_EN;
    }

    public static string GET_END_GAME_PANEL_LABEL_KILLS()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return END_GAME_PANEL_LABEL_KILLS_DE;
            case SystemLanguage.Turkish:
                return END_GAME_PANEL_LABEL_KILLS_TR;
        }
        return         END_GAME_PANEL_LABEL_KILLS_EN;
    }

    public static string GET_END_GAME_PANEL_LABEL_WINS()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return END_GAME_PANEL_LABEL_WINS_DE;
            case SystemLanguage.Turkish:
                return END_GAME_PANEL_LABEL_WINS_TR;
        }
        return         END_GAME_PANEL_LABEL_WINS_EN;
    }

    public static string GET_END_GAME_PANEL_LABEL_MONEY()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return END_GAME_PANEL_LABEL_MONEY_DE;
            case SystemLanguage.Turkish:
                return END_GAME_PANEL_LABEL_MONEY_TR;
        }
        return         END_GAME_PANEL_LABEL_MONEY_EN;
    }

    public static string GET_END_ROUND_PANEL_LABEL_T_WIN()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return END_ROUND_PANEL_LABEL_T_WIN_DE;
            case SystemLanguage.Turkish:
                return END_ROUND_PANEL_LABEL_T_WIN_TR;
        }
        return         END_ROUND_PANEL_LABEL_T_WIN_EN;
    }

    public static string GET_END_ROUND_PANEL_LABEL_CT_WIN()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return END_ROUND_PANEL_LABEL_CT_WIN_DE;
            case SystemLanguage.Turkish:
                return END_ROUND_PANEL_LABEL_CT_WIN_TR;
        }
        return         END_ROUND_PANEL_LABEL_CT_WIN_EN;
    }

    public static string GET_IS_CONNECTING_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return IS_CONNECTING_LABEL_DE;
            case SystemLanguage.Turkish:
                return IS_CONNECTING_LABEL_TR;
        }
        return         IS_CONNECTING_LABEL_EN;
    }
    
    public static string GET_IS_DISCONNECTING_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return IS_DISCONNECTING_LABEL_DE;
            case SystemLanguage.Turkish:
                return IS_DISCONNECTING_LABEL_TR;
        }
        return         IS_DISCONNECTING_LABEL_EN;
    }
    
    public static string GET_IS_SEARCHING_PLAYER_LABEL()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                return IS_SEARCHING_PLAYER_LABEL_DE;
            case SystemLanguage.Turkish:
                return IS_SEARCHING_PLAYER_LABEL_TR;
        }
        return         IS_SEARCHING_PLAYER_LABEL_EN;
    }
}
