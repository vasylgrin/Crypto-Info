   M S B A M L   `   `   `  ����F  BMyCryptoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MyCryptoApp.ViewModelsMyCryptoApp.ViewModels  5   	       MyCryptoApp.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;
viewmodels$clr-namespace:MyCryptoApp.ViewModels     5   	     � Title$  
MainWindow��5   	   $	��500��5	   	   $	��900��5
   	    � 
ResizeMode$ NoResize=�5   	   
������  � WindowStyle$ 
ToolWindow=�5   	    � DataContext 5      *  $MyCryptoApp.ViewModels.MainViewModel  5   
   .��5      ��  
  Border#��[  5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��75* �6#   �� 5      $
��239* �6#    � RowDefinitions 5      �� 5      $	��63* �6    �� 5      $
��437* �6    .+�5      �� $��Crypto Info��6   $��Center=�5       $��Center=�5!      $��15(�5"      $��Center=�5#      $��Center=�5$      $��30��5%      $��84��5&       	 Label#��[ 5'      �� 5)      $��1��6   $��2��6&   $��	0,0,685,0q�6:   .+�5+      	� $
��Home��6   $��50��5,        RadioButtonTheme#��[ 5-       d� 	IsChecked$
 TrueY�5.      ��5/      �� *HomePageButton+	� 51      $��Detail Information��6   $��50��52      #��[ 53      ��54      �� *DetailPageButton+	� 56      $��Convert��6   $��50��57      #��[ 58      ��59      �� *ConvertButton+	� 5:      $��Options��6   $��50��5;      #��[ 5<      ��5=      �� *OptionsButton+5>      �� 5@      $��1��5A      $��5q�5B      $��2��5C      ��5@      �� *CurrentPage+ J� Opacity 5D      �� *BorderOpacity+5E      5F      5G      