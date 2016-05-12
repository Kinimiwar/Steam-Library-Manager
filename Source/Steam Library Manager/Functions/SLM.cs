﻿using FontAwesome.WPF;
using System;
using System.Linq;
using System.Windows.Media;

namespace Steam_Library_Manager.Functions
{
    class SLM
    {
        public class Settings
        {
            public static Func<Definitions.Game, object> getSortingMethod()
            {
                Func<Definitions.Game, object> Sort;

                switch (Properties.Settings.Default.defaultGameSortingMethod)
                {
                    default:
                    case "appName":
                        Sort = x => x.appName;
                        break;
                    case "appID":
                        Sort = x => x.appID;
                        break;
                    case "sizeOnDisk":
                        Sort = x => x.sizeOnDisk;
                        break;
                }

                return Sort;
            }

            public static void parseLibraryContextMenuItems()
            {
                string[] menuItems = Properties.Settings.Default.libraryContextMenu.Split('|');

                foreach (string menuItem in menuItems)
                {
                    if (string.IsNullOrEmpty(menuItem))
                        continue;

                    Definitions.List.contextMenu cItem = new Definitions.List.contextMenu();
                    if (menuItem.Equals("separator", StringComparison.InvariantCultureIgnoreCase))
                    {
                        cItem.IsSeparator = true;
                        Definitions.List.libraryContextMenuItems.Add(cItem);
                    }
                    else
                    {
                        string[] Item = menuItem.Split(';');

                        foreach (string hardtonamethings in Item)
                        {
                            string[] itemDetails = hardtonamethings.Split(new char[] { '=' }, 2);
                            cItem.IconColor = (Brush)new BrushConverter().ConvertFromInvariantString("black");

                            switch (itemDetails[0].ToLowerInvariant())
                            {
                                case "text":
                                    cItem.Header = itemDetails[1];
                                    break;
                                case "action":
                                    cItem.Action = itemDetails[1];
                                    break;
                                case "iconcolor":
                                    cItem.IconColor = (Brush)new BrushConverter().ConvertFromInvariantString(itemDetails[1]);
                                    break;
                                case "icon":
                                    cItem.Icon = (FontAwesomeIcon)Enum.Parse(typeof(FontAwesomeIcon), itemDetails[1], true);
                                    break;
                                case "showToNormal":
                                    cItem.showToNormal = (Definitions.List.menuVisibility) Enum.Parse(typeof(Definitions.List.menuVisibility), itemDetails[1], true);
                                    break;
                                case "showToSLMBackup":
                                    cItem.showToSLMBackup = (Definitions.List.menuVisibility)Enum.Parse(typeof(Definitions.List.menuVisibility), itemDetails[1], true);
                                    break;
                                case "active":
                                    cItem.IsActive = bool.Parse(itemDetails[1]);
                                    break;
                            }
                        }

                        Definitions.List.libraryContextMenuItems.Add(cItem);
                    }
                }
            }

            public static void saveLibraryContextMenuItems()
            {
                string libraryContextMenu = "";
                foreach (Definitions.List.contextMenu cItem in Definitions.List.libraryContextMenuItems)
                {
                    if (cItem.IsSeparator)
                        libraryContextMenu += "separator|";
                    else
                    {
                        if (!string.IsNullOrEmpty(cItem.Header))
                            libraryContextMenu += $"text={cItem.Header}";

                        if (!string.IsNullOrEmpty(cItem.Action))
                            libraryContextMenu += $";action={cItem.Action}";

                        if (!string.IsNullOrEmpty(cItem.Icon.ToString()))
                            libraryContextMenu += $";icon={cItem.Icon}";

                        if (cItem.IconColor != null)
                            libraryContextMenu += $";iconcolor={cItem.IconColor}";


                        libraryContextMenu += $";showToNormal={cItem.showToNormal}";

                        libraryContextMenu += $";showToSLMBackup={cItem.showToSLMBackup}";

                        libraryContextMenu += $";active={cItem.IsActive}";

                        libraryContextMenu += "|";
                    }
                }

                Properties.Settings.Default.libraryContextMenu = libraryContextMenu;
            }

            public static void parseGameContextMenuItems()
            {
                string[] menuItems = Properties.Settings.Default.gameContextMenu.Split('|');

                foreach (string menuItem in menuItems)
                {
                    if (string.IsNullOrEmpty(menuItem))
                        continue;

                    Definitions.List.contextMenu cItem = new Definitions.List.contextMenu();

                    if (menuItem.Equals("separator", StringComparison.InvariantCultureIgnoreCase))
                    {
                        cItem.IsSeparator = true;
                        Definitions.List.gameContextMenuItems.Add(cItem);
                    }
                    else
                    {
                        string[] Item = menuItem.Split(';');

                        foreach (string hardtonamethings in Item)
                        {
                            string[] itemDetails = hardtonamethings.Split(new char[] { '=' }, 2);

                            cItem.IconColor = (Brush)new BrushConverter().ConvertFromInvariantString("black");

                            switch (itemDetails[0].ToLowerInvariant())
                            {
                                case "text":
                                    cItem.Header = itemDetails[1];
                                    break;
                                case "action":
                                    cItem.Action = itemDetails[1];
                                    break;
                                case "iconcolor":
                                    cItem.IconColor = (Brush)new BrushConverter().ConvertFromInvariantString(itemDetails[1]);
                                    break;
                                case "icon":
                                    cItem.Icon = (FontAwesomeIcon)Enum.Parse(typeof(FontAwesomeIcon), itemDetails[1], true);
                                    break;
                                case "showToNormal":
                                    cItem.showToNormal = (Definitions.List.menuVisibility)Enum.Parse(typeof(Definitions.List.menuVisibility), itemDetails[1], true);
                                    break;
                                case "showToSLMBackup":
                                    cItem.showToSLMBackup = (Definitions.List.menuVisibility)Enum.Parse(typeof(Definitions.List.menuVisibility), itemDetails[1], true);
                                    break;
                                case "showToSteamBackup":
                                    cItem.showToSteamBackup = (Definitions.List.menuVisibility)Enum.Parse(typeof(Definitions.List.menuVisibility), itemDetails[1], true);
                                    break;
                                case "showToCompressed":
                                    cItem.showToCompressed = (Definitions.List.menuVisibility)Enum.Parse(typeof(Definitions.List.menuVisibility), itemDetails[1], true);
                                    break;
                                case "active":
                                    cItem.IsActive = bool.Parse(itemDetails[1]);
                                    break;
                            }
                        }

                        Definitions.List.gameContextMenuItems.Add(cItem);
                    }
                }
            }

            public static void saveGameContextMenuItems()
            {
                string gameContextMenu = "";
                foreach (Definitions.List.contextMenu cItem in Definitions.List.gameContextMenuItems)
                {
                    if (cItem.IsSeparator)
                        gameContextMenu += "separator|";
                    else
                    {
                        if (!string.IsNullOrEmpty(cItem.Header))
                            gameContextMenu += $"text={cItem.Header}";

                        if (!string.IsNullOrEmpty(cItem.Action))
                            gameContextMenu += $";action={cItem.Action}";

                        gameContextMenu += $";icon={cItem.Icon}";

                        if (cItem.IconColor != null)
                            gameContextMenu += $";iconcolor={cItem.IconColor}";

                        gameContextMenu += $";showToNormal={cItem.showToNormal}";
                        gameContextMenu += $";showToSLMBackup={cItem.showToSLMBackup}";
                        gameContextMenu += $";showToSteamBackup={cItem.showToSteamBackup}";
                        gameContextMenu += $";showToCompressed={cItem.showToCompressed}";

                        gameContextMenu += $";active={cItem.IsActive}";

                        gameContextMenu += "|";
                    }
                }

                Properties.Settings.Default.gameContextMenu = gameContextMenu;
            }

            public static void updateBackupDirs()
            {
                try
                {
                    // Define a new string collection to update backup library settings
                    System.Collections.Specialized.StringCollection BackupDirs = new System.Collections.Specialized.StringCollection();

                    // foreach defined library in library list
                    foreach (Definitions.Library Library in Definitions.List.Libraries.Where(x => x.Backup))
                    {
                        // then add this library path to new defined string collection
                        BackupDirs.Add(Library.fullPath);
                    }

                    // change our current backup directories setting with new defined string collection
                    Properties.Settings.Default.backupDirectories = BackupDirs;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
            }

            public static void saveSettings()
            {
                saveLibraryContextMenuItems();
                saveGameContextMenuItems();

                Properties.Settings.Default.Save();
            }
        }

        public static void onLoaded()
        {
            Steam.updateSteamInstallationPath();

            Settings.parseLibraryContextMenuItems();
            Settings.parseGameContextMenuItems();

            Library.generateLibraryList();
        }

        public static void onClosing()
        {
            Settings.updateBackupDirs();
            Settings.saveSettings();
        }

    }
}
