   M S B A M L   `   `   `  ����F  BMyCryptoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MyCryptoApp.ViewModelsMyCryptoApp.ViewModels  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      %    MyCryptoApp.Views.Pages.Convert   5      W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;
viewmodels$clr-namespace:MyCryptoApp.ViewModels     5      Dlcv4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      $	��800��5	      $	��450��5
      
������   � DataContext  5      -  'MyCryptoApp.ViewModels.ConvertViewModel  5   
   .��5      �� .+�5   
   �� $��Convert��6   $��28(�5      $
��Left=�5      $��0,0,0,20q�5         	Textblock#��[  5      �� 5   
    �� Orientation$ 
Horizontal=�6   .+�5      �� $	��200��6   $
��Left=�5      ._�5      �� $��That we convert��6   $��20(�5      $
��Left=�5      $��0,0,0,20q�5      #��[  5      5       �� 5!      $	��200��6   $��20,0,0,0q�5"      ._�5#      �� $��What do we convert to��6   $��20(�5$      $
��Left=�5%      $��0,0,0,20q�5&      #��[  5'      5(      �� 5)      $	��200��6   $��20,0,0,0q�5*      ._�5+      �� $��Convert token��6   $��20(�5,      $
��Left=�5-      $��0,0,0,20q�5.      #��[  5/      50      51      �� 52   
   $ 
Horizontal=�6   $��20��53      .+�54      �� $��80��6   
������� J� Clip 56      �� 57       �� RadiusX$ 10Q�6(    �� RadiusY$ 10Q�58   (    �� Rect$ 	0,0,80,20�59   (   ._�5;      �� -   5        $��BaseIdTextBlock��5=      $��0q�5<      ��5;      �� *
FirstToken+5=      5>      �� 5?      $��40��6   
������� 5A      �� 5B      $ 10Q�6(   $ 10Q�5C   (   $ 	0,0,40,20�5D   (   ._�5F      �� $��0q�5G      ��5F      �� *FirstValueToken+5G      5H      �� 5I      $��80��6   
�������$��	100,0,0,0q�5K       5L      �� 5M      $ 10Q�6(   $ 10Q�5N   (   $ 	0,0,80,20�5O   (   ._�5Q      �� -   5        $��QuoteIdTextBlock��5S      $��0q�5R      ��5Q      �� *SecondToken+5S      5T      �� 5U      $	��150��6   
�������$��	140,0,0,0q�5W       5X      �� 5Y      $ 10Q�6(   $ 10Q�5Z   (   $ 
0,0,150,20�5[   (   ._�5]      �� 
������ ��6   �� *PrintConvertToken+5^      5_      �� 5`      $��50��6   
�������$��10,0,0,0q�5b       5c      �� 5d      $ 10Q�6(   $ 10Q�5e   (   $ 	0,0,50,20�5f   (   ._�5h      �� 
������ $��0q�5i      ��5j      �� *ConvertToken+.��5k      �� $��Convert��6    5l      5m      5n      �� 5o   
   $	��270��6   $	��650��5p      $
��Left=�5q      $��0,10,0,0q�5r      .+�5s      #  LiveCharts.Wpf.CartesianChart  $	��270��6!   &   LiveCharts.Wpf.Charts.Base.Chart   Series 5t   !   �� *SeriesCollection+5u      �� 5v   
   $��35��6   $ 
Horizontal=�5w      .+�5x      �� -   5        $��BorderOpacityClose��5~      
�������$	��200��5y      $��35��5z      $
��Left=�5{      $��0,0,50,0q�5|       J� Opacity 5}      �� *ErrorOpacityProperty+ 5      �� 5�      $ 10Q�6(   $ 10Q�5�   (   $ 
0,0,200,35�5�   (   ._�5�      �� 
������ 
���   �$��Center=�5�      $��Center=�5�      ��5�      �� *	ErrorText+5�      5�      5�      5�      5�      