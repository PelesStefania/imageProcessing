   M S B A M L   `   `   `  ����D  @Framework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Framework.ViewModelFramework.ViewModel  5   	   &     Framework.View.ColorLevelsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:Framework.ViewModel     5   	   R NOxyPlot.Wpf, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf   5   	   -   5        $	��550��5   	   $
��1200��5   	     � WindowStartupLocation$  CenterScreen=�5   	    � 
ResizeMode$ CanMinimize=�5   	    � Topmost .  � Icon$ ../Images/Icon.png��5   	    � Title 5	   	   ��  �� Path$ Title� � DataContext 5      '   !Framework.ViewModel.ColorLevelsVM  5   
   .��5      � ��6   �� $ Theme.BackgroundColor� � RowDefinitions 5   
   �� 5      $��15 �6   �� 5      $��40 �6   �� 5      $��* �6   �� 5      $��15 �6    � ColumnDefinitions 5   
   �� 5      $��15 �6   �� 5      $	��450 �6   �� 5       $��* �6   �� 5!      $	��450 �6   �� 5"      $��15 �6   .+�5%   
   �� $��2��6   $��1��5&      .��5'        OxyPlot.Wpf.PlotView  -   5        $��initialImageView��5(      
�������$	��440��5)      $	��440��5*      5+      �� 5,   
   $��2��6   $��3��5-      .��5.        -   5        $��processedImageView��5/      
�������$	��440��50      $	��440��51      52      �� 54   
   $��1��6   $��1��55      $��Initial Image��56      $��15(�57      $	��120��59      $��20��5:      $��Center=�5;      ��58      �� $ Theme.TextForeground�5;      �� 5<   
   $��1��6   $��3��5=      $��Processed Image��5>      $	��120��5?      $��20��5@      $��15(�5A      $��Center=�5C      ��5B      �� $ Theme.TextForeground�5C      �� 5E   
   $��1��6   $��2��5F      $��Settings��5G      $��60��5H      $��20��5I      $��15(�5J      $��Center=�5L      ��5K      �� $ Theme.TextForeground�5L      �� 5M   
   $��2��6   $��2��5N      $��30��5Q      $��70��5R      $��15(�5S      $��10,10,138.667,414q�5T      	 �� TextAlignment$	 Center=�5U      ��5O      �� $
 XPos���5P      �� $ Theme.TextForeground�5U      �� 5V   
   $��2��6   $��2��5W      $��70��5Z      $��30��5[      $��15(�5\      $��139,10,9.667,414q�5]      $	 Center=�5^      ��5X      �� $
 YPos���5Y      �� $ Theme.TextForeground�5^      � 5`   
   $��2��6   $��2��5a      $��Center=�5b      $��Center=�5c      .+�5d      �� -   5        $��checkBoxBlue��5f      $��2��5d      $��2��5e      $��Blue Channel��5g      $
��Left=�5i      $��Center=�5j      $��	0,0,0,300q�5k      $��30��5l      
 d� 	IsChecked$

 TrueY�5m      $y�Hidden=�5p      ��5h      �� $ Theme.TextForeground�5p      �� 5q      -   5        $��checkBoxGreen��5s      $��2��5q      $��2��5r      $��Green Channel��5t      $
��Left=�5v      $��Center=�5w      $��	0,0,0,200q�5x      $��30��5y      $

 TrueY�5z      $y�Hidden=�5}      ��5u      �� $ Theme.TextForeground�5}      �� 5~      -   5        $��checkBoxRed��5�      $��2��5~      $��2��5      $��Red Channel��5�      $
��Left=�5�      $��Center=�5�      $��	0,0,0,100q�5�      $��30��5�      $

 TrueY�5�      $y�Hidden=�5�      ��5�      �� $ Theme.TextForeground�5�      5�      5�      5�      