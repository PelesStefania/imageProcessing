   M S B A M L   `   `   `  ����D  @Framework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Framework.ViewModelFramework.ViewModel  5   	   !    Framework.View.DialogWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:Framework.ViewModel     5   	     � Title$  
Dialog Box��5   	   $	��100��5	   	   $	��400��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ CanMinimize=�5   	    � Topmost .  � Icon$ ../Images/Icon.png��5   	   ��5
   	   ��  �� Path$ Height� �� Mode$ TwoWay=� � DataContext 5      "  Framework.ViewModel.DialogVM  5   
   .��5      � ��6   �� $ Theme.BackgroundColor� � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $��20 �6   	 � ColumnDefinitions	 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5       $��20 �6   .+�5#   
   �� $��1��6   $��1��5$      ��5%      �� $ 
Parameters���5&      �� 5'      .R�5(      �� .+�5)      �� ��6$   �� $ 	ParamText���5*   $   �� $ Height���5+   $   �� $ 
Foreground��� 5-      ��6"   �� $ 	InputText���5.   "   �� $ Height�5/      50      52      �� 54   
   $��2��6   $��1��55      $
��Done��56      $��25��57      $��50��58      $��2q�59      ��5:      �� $! Theme.ButtonForegroundColor���5;      �� $! Theme.ButtonBackgroundColor���5<      �� $ Theme.ButtonBorderBrush���5=      �� $ CloseCommand�5>      5?      