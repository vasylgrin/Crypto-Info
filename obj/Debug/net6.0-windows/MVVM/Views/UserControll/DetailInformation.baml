   M S B A M L   `   `   `  ����F  BMyCryptoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyCryptoApp.PagesMyCryptoApp.Pages  5      ?$clr-namespace:MyCryptoApp.ViewModelsMyCryptoApp.ViewModels  65   )    #MyCryptoApp.Pages.DetailInformation   5      W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:MyCryptoApp.Pages     5      ;
viewmodels$clr-namespace:MyCryptoApp.ViewModels     65   $	��800��5      $	��450��5	      
������   � DataContext  5      ;  5MyCryptoApp.ViewModels.DetailInformationPageViewModel  5   
   .��5      �� .+�5   
   �� $��Detail Information��6   $��28(�5      $
��Left=�5      $��0,0,0,20q�5         	Textblock#��[  5      �� 5   
   .+�5      �� $��Enter token for search��6   $��20(�5      $
��Left=�5      $��0,0,0,20q�5      #��[  5      5      �� 5   
    �� Orientation$ 
Horizontal=�6   .+�5      �� $	��200��6   $��35��5       
������� J� Clip 5"      �� 5#       �� RadiusX$ 10Q�6(    �� RadiusY$ 10Q�5$   (    �� Rect$ 
0,0,200,35�5%   (   ._�5'      � .+�5(      �� $��0q�6   
������ ��5*      �� *TokenToSearch+.��6=   5+      5,      5-      �� 5.      $	��100��6   $��35��5/      $��10,0,0,0q�50       51      �� 52      $ 10Q�6(   $ 10Q�53   (   $ 
0,0,100,35�54   (   ._�56      �� $��0q�6   
���������58      �� *SearchTokenButton+.��59      �� $��Search��6    $��Center=�5:       $��Center=�5;       5<      5=      5>      �� 5?   
   $��10��6   �� 5@   
   $��20��6   $ 
Horizontal=�5A      .+�5C      �� $	��250��6   $��20��5D      $	��Top=�5E       5F      �� 5G      $ 10Q�6(   $ 10Q�5H   (   $ 
0,0,250,20�5I   (   ._�5K      � .+�5L      �� 
���������5M       �� *GetSearchToken+5N      5O      5R      �� 5S   
   $��40��6   �� 5T   
   $ 
Horizontal=�6   $	��229��5U      .+�5V      �� $	��600��6   $	��229��5W      
������� 5Y      �� 5Z      $ 10Q�6(   $ 10Q�5[   (   $ 0,0,600,229�5\   (   ._�5_      �� $��1��6   $��Hidden=�5a      $��Hidden=�5b      
������ $��0q�5d      ��5`      �� *	Markets+��5f      �� 5g      .R�5h      � $��40��6#   $	��509��6/    � ColumnDefinitions 5i   "   �� 5j   &   �� 5k   &   �� 5l   &   �� 5m   &   �� 5n   &   �� 5o   &   .+�5r   "   �� $��5,5,5,5q�6,   ��5s   $   �� *Name+�� 5u   "   $��5,5,5,5q�6,   $��1��5v   $   ��5w   $   �� *	TokenName+�� 5y   "   $��5,5,5,5q�6,   $��2��5z   $   ��5{   $   �� *TokenSymbol+�� 5}   "   $��5,5,5,5q�6,   $��3��5~   $   ��5   $   �� *	QuoteId+�� 5�   "   $��5,5,5,5q�6,   $��4��5�   $   ��5�   $   �� *QuoteSymbol+�� 5�   "   $��5,5,5,5q�6,   $��5��5�   $   ��5�   $   �� *
PriceUsd+5�      5�      5�      5�      5�      5�      5�      