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
    private static string NAME_TR = "İsim";
    
    // Setting Names
    private static string PLAYER_STATUS_NO_NAME_ME_EN = "Enter your name";
    private static string PLAYER_STATUS_NO_NAME_ME_DE = "Geben Sie Ihren Namen ein";
    private static string PLAYER_STATUS_NO_NAME_ME_TR = "İsminizi girin";
    
    private static string PLAYER_STATUS_NO_NAME_TEAM_EN = "Enter a name for your teammate";
    private static string PLAYER_STATUS_NO_NAME_TEAM_DE = "Geben Sie einen Namen für Ihren Teamkollegen ein";
    private static string PLAYER_STATUS_NO_NAME_TEAM_TR = "Takım arkadaşınıza bir isim verin";
    
    // Main Menu Buttons
    private static string RANKED_BUTTON_LABEL_EN = "Ranked";
    private static string RANKED_BUTTON_LABEL_DE = "Wettkampf";
    private static string RANKED_BUTTON_LABEL_TR = "Rekabetçi";
    
    private static string RANKED_OFFLINE_BUTTON_LABEL_EN = "War Game";
    private static string RANKED_OFFLINE_BUTTON_LABEL_DE = "Kriegspiel";
    private static string RANKED_OFFLINE_BUTTON_LABEL_TR = "Savaş Oyunu";
    
    private static string TEAM_EDITOR_BUTTON_LABEL_EN = "Team Editor";
    private static string TEAM_EDITOR_BUTTON_LABEL_DE = "Team Editor";
    private static string TEAM_EDITOR_BUTTON_LABEL_TR = "Takım Ayarları";
    
    private static string INVENTORY_BUTTON_LABEL_EN = "Inventory";
    private static string INVENTORY_BUTTON_LABEL_DE = "Inventar";
    private static string INVENTORY_BUTTON_LABEL_TR = "Envanter";
    
    private static string REMOVE_ADS_BUTTON_LABEL_EN = "Remove Ads";
    private static string REMOVE_ADS_BUTTON_LABEL_DE = "Werbung Entfernen";
    private static string REMOVE_ADS_BUTTON_LABEL_TR = "Reklamları Kaldır";
    
    private static string MORE_COIN_BUTTON_LABEL_EN = "Buy Coins";
    private static string MORE_COIN_BUTTON_LABEL_DE = "Kaufe Münzen";
    private static string MORE_COIN_BUTTON_LABEL_TR = "Jeton Satın Al";
    
    private static string RESTORE_PURCHASE_BUTTON_LABEL_EN = "Restore Purchase";
    private static string RESTORE_PURCHASE_BUTTON_LABEL_DE = "Kauf wiederherstellen";
    private static string RESTORE_PURCHASE_BUTTON_LABEL_TR = "Satın Alma İşlemini Geri Yükle";
    
    // Other Menu Buttons
    private static string START_SEARCH_BUTTON_LABEL_EN = "START";
    private static string START_SEARCH_BUTTON_LABEL_DE = "LOS";
    private static string START_SEARCH_BUTTON_LABEL_TR = "BAŞLAT";
    
    private static string CANCEL_SEARCH_BUTTON_LABEL_EN = "CANCEL";
    private static string CANCEL_SEARCH_BUTTON_LABEL_DE = "ABBRECHEN";
    private static string CANCEL_SEARCH_BUTTON_LABEL_TR = "İPTAL";
    
    private static string STORE_TAB_BUTTON_LABEL_EN = "Store";
    private static string STORE_TAB_BUTTON_LABEL_DE = "Laden";
    private static string STORE_TAB_BUTTON_LABEL_TR = "Mağaza";
    
    private static string INVENTORY_TAB_BUTTON_LABEL_EN = "Inventory";
    private static string INVENTORY_TAB_BUTTON_LABEL_DE = "Inventar";
    private static string INVENTORY_TAB_BUTTON_LABEL_TR = "Envanter";
    
    private static string STORE_BUY_BUTTON_LABEL_EN = "Buy";
    private static string STORE_BUY_BUTTON_LABEL_DE = "Kaufen";
    private static string STORE_BUY_BUTTON_LABEL_TR = "Satın al";
    
    private static string STORE_Bought_BUTTON_LABEL_EN = "Already purchased";
    private static string STORE_Bought_BUTTON_LABEL_DE = "Bereits gekauft";
    private static string STORE_Bought_BUTTON_LABEL_TR = "Satın alındı";
    
    // Popup Menus & Buttons
    private static string EQUIP_MENU_TITLE_EN = "Equip";
    private static string EQUIP_MENU_TITLE_DE = "Ausrüsten";
    private static string EQUIP_MENU_TITLE_TR = "Kuşan";
    
    private static string EQUIP_MENU_BUTTON_LABEL_T_EN = "Replace for T";
    private static string EQUIP_MENU_BUTTON_LABEL_T_DE = "Für T ausrüsten";
    private static string EQUIP_MENU_BUTTON_LABEL_T_TR = "T için değiştir";
    
    private static string EQUIP_MENU_BUTTON_LABEL_CT_EN = "Replace for CT";
    private static string EQUIP_MENU_BUTTON_LABEL_CT_DE = "Für CT ausrüsten";
    private static string EQUIP_MENU_BUTTON_LABEL_CT_TR = "CT için değiştir";
    
    private static string EQUIP_MENU_BUTTON_LABEL_BOTH_EN = "Replace for Both Teams";
    private static string EQUIP_MENU_BUTTON_LABEL_BOTH_DE = "Für beide Teams ausrüsten";
    private static string EQUIP_MENU_BUTTON_LABEL_BOTH_TR = "Her iki takım için değiştir";
    
    private static string EQUIP_MENU_BUTTON_LABEL_CANCEL_EN = "Cancel";
    private static string EQUIP_MENU_BUTTON_LABEL_CANCEL_DE = "Abbrechen";
    private static string EQUIP_MENU_BUTTON_LABEL_CANCEL_TR = "İptal";
    

    private static string EXIT_GAME_MENU_TITLE_EN = "Do you wish to stop playing now?\nYou will lose the game if you leave.";
    private static string EXIT_GAME_MENU_TITLE_DE = "Möchten Sie das Spiel beenden?\nSie werden das Spiel verlieren wen Sie das Spiel beenden.";
    private static string EXIT_GAME_MENU_TITLE_TR = "Oyundan çıkmak istiyor musun?\nOyundan ayrılırsan kaybedersin.";
    
    private static string EXIT_GAME_MENU_BUTTON_LABEL_YES_EN = "Yes";
    private static string EXIT_GAME_MENU_BUTTON_LABEL_YES_DE = "Ja";
    private static string EXIT_GAME_MENU_BUTTON_LABEL_YES_TR = "Evet";
    
    private static string EXIT_GAME_MENU_BUTTON_LABEL_NO_EN = "No";
    private static string EXIT_GAME_MENU_BUTTON_LABEL_NO_DE = "Nein";
    private static string EXIT_GAME_MENU_BUTTON_LABEL_NO_TR = "Hayır";


    private static string MORE_PLAYS_MENU_TITLE_EN = "Get more plays.";
    private static string MORE_PLAYS_MENU_TITLE_DE = "Holen Sie sich mehr play.";
    private static string MORE_PLAYS_MENU_TITLE_TR = "Daha fazla oyun hakkı edinin";
    
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_BUY_NO_PLAYS_EN = "Get unlimited plays";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_BUY_NO_PLAYS_DE = "Kaufe unbegrenzte plays";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_BUY_NO_PLAYS_TR = "Sınırsız oyun hakkı edin";
    
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_BUY_20_PLAYS_EN = "Buy 20 plays";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_BUY_20_PLAYS_DE = "Kaufe 20 plays";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_BUY_20_PLAYS_TR = "20 oyun hakkı satın al";
    
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_WATCH_AD_EN = "Watch ad";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_WATCH_AD_DE = "Werbung ansehen";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_WATCH_AD_TR = "Reklam izleyin";
    
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_CLOSE_EN = "Close";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_CLOSE_DE = "Schließen";
    private static string MORE_PLAYS_MENU_BUTTON_LABEL_CLOSE_TR = "Kapat";
    
    
    private static string SEARCH_GAME_MENU_TITLE_EN = "YOUR MATCH IS READY";
    private static string SEARCH_GAME_MENU_TITLE_DE = "IHR SPIEL IST BEREIT";
    private static string SEARCH_GAME_MENU_TITLE_TR = "MAÇIN HAZIR";
    
    private static string SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPT_EN = "ACCEPT";
    private static string SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPT_DE = "ANNEHMEN";
    private static string SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPT_TR = "KABUL ET";
    
    private static string SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPTED_EN = "ACCEPTED";
    private static string SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPTED_DE = "ANGENOMMEN";
    private static string SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPTED_TR = "KABUL EDİLDİ";
    
    
    private static string SET_TEAM_NAMES_MENU_TITLE_EN = "You have to name yourself and your teammates.";
    private static string SET_TEAM_NAMES_MENU_TITLE_DE = "Sie müssen sich und Ihre Teamkollegen benennen.";
    private static string SET_TEAM_NAMES_MENU_TITLE_TR = "Kendinize ve takım arkadaşlarınıza isim vermek zorundasınız.";
    
    private static string SET_TEAM_NAMES_MENU_BUTTON_LABEL_CLOSE_EN = "Close";
    private static string SET_TEAM_NAMES_MENU_BUTTON_LABEL_CLOSE_DE = "Schließen";
    private static string SET_TEAM_NAMES_MENU_BUTTON_LABEL_CLOSE_TR = "Kapat";
    
    private static string SET_TEAM_NAMES_MENU_BUTTON_LABEL_SET_TEAM_EN = "Team settings";
    private static string SET_TEAM_NAMES_MENU_BUTTON_LABEL_SET_TEAM_DE = "Team einstellungen";
    private static string SET_TEAM_NAMES_MENU_BUTTON_LABEL_SET_TEAM_TR = "Takım ayarları";
    
    // Game Texts & Buttons
    private static string BUY_PANEL_BUTTON_LABEL_FLASHBANG_EN = "Flashbang";
    private static string BUY_PANEL_BUTTON_LABEL_FLASHBANG_DE = "Blendgranate";
    private static string BUY_PANEL_BUTTON_LABEL_FLASHBANG_TR = "Flaş Bombası";
    
    private static string BUY_PANEL_BUTTON_LABEL_ZEUS_EN = "Zeus x27";
    private static string BUY_PANEL_BUTTON_LABEL_ZEUS_DE = "Zeus x27";
    private static string BUY_PANEL_BUTTON_LABEL_ZEUS_TR = "Zeus x27";
    
    private static string BUY_PANEL_BUTTON_LABEL_DEFUSE_EN = "Defuse Kit";
    private static string BUY_PANEL_BUTTON_LABEL_DEFUSE_DE = "Entschärfungskit";
    private static string BUY_PANEL_BUTTON_LABEL_DEFUSE_TR = "İmha Teçhizatı";
    
    
    private static string SHOW_WEAPON_PANEL_TITLE_START_EN = "You were killed by"; // Mali's
    private static string SHOW_WEAPON_PANEL_TITLE_MIDDLE_EN = "'s";
    private static string SHOW_WEAPON_PANEL_TITLE_END_EN = "weapon";
    
    private static string SHOW_WEAPON_PANEL_TITLE_START_DE = "Du wurdest von"; // Malis
    private static string SHOW_WEAPON_PANEL_TITLE_MIDDLE_DE = "s";
    private static string SHOW_WEAPON_PANEL_TITLE_END_DE = "Waffe getötet";
    
    private static string SHOW_WEAPON_PANEL_TITLE_START_TR = "";
    private static string SHOW_WEAPON_PANEL_TITLE_MIDDLE_TR = "";
    private static string SHOW_WEAPON_PANEL_TITLE_END_TR = "seni silahıyla öldürdü";

    
    private static string END_GAME_PANEL_LABEL_KILLS_EN = "Kills : ";
    private static string END_GAME_PANEL_LABEL_KILLS_DE = "Abschüsse : ";
    private static string END_GAME_PANEL_LABEL_KILLS_TR = "Leşler : ";
    
    private static string END_GAME_PANEL_LABEL_WINS_EN = "Wins : ";
    private static string END_GAME_PANEL_LABEL_WINS_DE = "Gewinne : ";
    private static string END_GAME_PANEL_LABEL_WINS_TR = "Kazanmalar : ";
    
    private static string END_GAME_PANEL_LABEL_MONEY_EN = "Coins : " + GET_CURRENCY();
    private static string END_GAME_PANEL_LABEL_MONEY_DE = "Münzen : " + GET_CURRENCY();
    private static string END_GAME_PANEL_LABEL_MONEY_TR = "Jetonlar" + GET_CURRENCY();
    
    
    private static string END_ROUND_PANEL_LABEL_T_WIN_EN = "Terrorists Win";
    private static string END_ROUND_PANEL_LABEL_T_WIN_DE = "Terroristen Gewinnen";
    private static string END_ROUND_PANEL_LABEL_T_WIN_TR = "Teröristler Kazandı";
    
    private static string END_ROUND_PANEL_LABEL_CT_WIN_EN = "Counter-Terrorists Win";
    private static string END_ROUND_PANEL_LABEL_CT_WIN_DE = "Antiterroreinheit Gewinnen";
    private static string END_ROUND_PANEL_LABEL_CT_WIN_TR = "Anti-Teröristler Kazandı";
    
    // Connections
    
    private static string IS_CONNECTING_LABEL_EN = "Connecting to server";
    private static string IS_CONNECTING_LABEL_DE = "Verbindung zum Server wird hergestellt";
    private static string IS_CONNECTING_LABEL_TR = "Sunucuya bağlanılıyor";
    
    private static string IS_DISCONNECTING_LABEL_EN = "Disconnecting from server";
    private static string IS_DISCONNECTING_LABEL_DE = "Verbindung zum Server wird getrennt";
    private static string IS_DISCONNECTING_LABEL_TR = "Sunucu bağlantısı kesiliyor";
    
    private static string IS_SEARCHING_PLAYER_LABEL_EN = "Connecting to player";
    private static string IS_SEARCHING_PLAYER_LABEL_DE = "Suche nach Spieler";
    private static string IS_SEARCHING_PLAYER_LABEL_TR = "Oyuncu aranıyor";
    
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
