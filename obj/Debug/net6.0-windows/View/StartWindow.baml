   M S B A M L   `   `   `  ����L  HCrossesAndNoughts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:CrossesAndNoughts.ViewCrossesAndNoughts.View  5   	   I)clr-namespace:CrossesAndNoughts.ViewModelCrossesAndNoughts.ViewModel  6<   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 6x   (    "CrossesAndNoughts.View.StartWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:CrossesAndNoughts.View     5   	   ?	viewmodel)clr-namespace:CrossesAndNoughts.ViewModel     6<   4s&clr-namespace:System;assembly=mscorlib     6x     � Title$  Crosses and Noughts��5   	   $	��450��6%   $	��800��62   
����ح�$	��470��6U   $	��690��6e    � 	Resources 5	         ButtonStyle �� Property �� Value��%�   &	        �� 5
   
   .�5      ��  � ��$ Impact)�68   �� 5       � ��
 ��d�68   �� 5       � ��
 ��=H�68   �� 5       � ��$ 30(�66   5      5      .��5      � -   5         �Name$ 
LayoutGrid��5       � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��2* �6   �� 5      $��75 �6   .+�5   
   �� $��1��6   $��2��6!   $��10q�6.   $��
Start Game��6:   #��[  6O    � Cursor$
 Hand��5      ��6$   �� *GoNextCommand+��6F   ��  �� ElementName$ 
LoginLabel���� 5!   
   $��0��6   $��2��6!   $��10q�6.   $
��Quit��6:   #��[  6I   $
 Hand��5"      ��6$   �� *QuitCommand+�� 5$   
   $��2��6   $��2��6!   $��10q�6.   $��Records��6:   #��[  6L   $
 Hand��5%      ��6$   �� *GoNextCommand+��6F   �� $ RecordsLabel��(� 5'   
   $��3��6   $��5q�6&   $��0��61   .��5(      �� $��Crosses and Noughts��6   $��Impact)�63   
����=H�	 �� TextAlignment$	 Center=�6b   5)      �� 5+   
   $%��/Resources/Images/TicTacToe.png��6   $��1��69   $��1��6I   $	��800��6V   $	��800��6f   �� 5-   
   -   5        
 ��Name$
 RecordsLabel��5-   '   $y�	Collapsed=�6   
����ح�$��0��6T   $��0��6d   $��3��6q   $��3��6�   CollapsedAtStart����5.      � 5/      $��Center=�6�   $��Center=�6�   ��6   �� $ RecordsLabel�� �� Path$ ActualHeight���6V   �� $ RecordsLabel��$ ActualWidth� 50      �� 51      $��* �6+   �� 52      $��2* �6+   �� 53      $��* �6+    56      �� 57      $��112.5 �6(   �� 58      $��* �6(   �� 59      $��112.5 �6(   .+�5<      &   System.Windows.Controls.DataGrid  $��1��6   $��1��6/      
IsReadOnly$
 True��6<   
����·�
����=H�   RowBackground
 �������6�   �� *	Records+�� 5>      $��1��6   $��2��6-   $��100, 20, 100, 40q�6:   $
��Back��6T   #��[  6c   $
 Hand��6�   ��6�   �� *GoBackCommand+��6�   �� $ RecordsLabel��5?      5A      �� 5C   
   -   5        $
 
LoginLabel��5C   '   $y�	Collapsed=�6   
�������$��100, 75, 100, 75q�6O   $��0��6i   $��0��6y   $��3��6�   $��3��6�   
����=H�$��2q�6�   CollapsedAtStart��$	��450��6�   $	��800��6  ��5D      � 5E      $��Center=�6�   $��Center=�6�   ��6   �� $ 
LoginLabel��$ ActualHeight���6T   �� $ 
LoginLabel��$ ActualWidth� 5F      �� 5G      $��* �6+   �� 5H      $��* �6+    5J      �� 5K      $��80 �6(   �� 5L      $��* �6(   �� 5M      $��80 �6(   .+�5P      �� $��Enter your username:��6    $��Impact)�6<   
����=H�$��2��6k   $��Center=�6   $��Center=�6�   $��40��6�   $��40(�6�   $	 Center=�6�   (� 5R      $��1��6   $��100, 30, 100, 30q�6+   $��3��6E   $	��150��6Y   $��44��6i   $
��1000��6x   .��5S      �� $��1��6"   $��30(�6/   
����=H�$��2q�6Y    �� TextAlignment$ Center=�6m   $��Center=�6�   $ IBeam��6�    �� MaxLines$ 1��6�    �� 	MaxLength$ 20��6�   $	��300��6�   $��50��6�   $��5q�6�   5T      �� 5V      $��2��6   $��10q�6*   $
��Back��66   #��[  6E   $
 Hand��6j   ��6x   �� *GoBackCommand+��6�   �� $ 
LoginLabel���� 5X      $��2��6   $��1��6*   $��10q�6:   $��
Start Game��6F   #��[  6[   $
 Hand��6�   ��6�   �� *StartGameCommand+5Y      5[      5\      5]      