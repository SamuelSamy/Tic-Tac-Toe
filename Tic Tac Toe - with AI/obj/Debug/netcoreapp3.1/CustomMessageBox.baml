   M S B A M L   `   `   `  ����P  LTic Tac Toe - with AI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Tic_Tac_Toe___with_AITic_Tac_Toe___with_AI  5   	   ,    &Tic_Tac_Toe___with_AI.CustomMessageBox   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Tic_Tac_Toe___with_AI     5   	   -   5          � Title$  CustomMessageBox��5   	   $	��800��69   $	��396��6E   $y�Visible=�6R    J� Opacity$ 0Q�6g    � WindowStartupLocation$ CenterOwner=�6s    � 
ResizeMode$ NoResize=�6�    � ShowInTaskbar$ False��6�    � WindowState$ Normal=�6�   $��Center=�6�   $��Center=�6
   � AllowsTransparency$
 True��6'   � WindowStyle$
 None=�6A  ��6"   Z� .��5	      � -   5         �Name$ MainGrid��5	      .+�5
   
   �� -   5        	 ��Name$	 Border��5
      ���	#FFFF6464
 �� CornerRadius$	
 100��68   � 5   
   -   5        $ 
BorderGrid��5       � RowDefinitions 5      �� 5      $��1* �6    �� 5      $��5* �6    �� 5      $��1.75* �6    .+�5      �� -   5        $��	TextBlock��5      $,��&It's a tie! Do you want to play again?��6)   $��WrapWithOverflow=�6W   $��1��6w   $��
50,81,50,0q�6�   $��Verdana)�6�   $��48(�6�   $
��None=�6�    �� TextAlignment$ Center=�6�   $��Normal"�6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      � 5      -   5        $ ButtonsGrid��5      $��2��6(   $��0,5,0,5q�65    � ColumnDefinitions 5      �� 5      $��1* �6'   �� 5      $
��1.5* �6'   �� 5      $��1* �6'   �� 5       $
��1.5* �6'   �� 5!      $��1* �6'   .+�5&      �� -   5         ��Name$ btnYes��5&      $��1��6)   $	��Yes��69   
�������
�������$��Modern No. 20)�6�   $
��Bold"�6�   $��48(�6�   ��6Z   Z�  � 	Resources 5'       �� 
TargetType �� Property �� Value��%_   '��       �� 5(      " ��6    .�5)      ��  �@��CornerRadius$ 25��6=   5*      5+       5,        5-      5/      �� 51      -   5        $ btnNo��51      $��3��6(   $��No��68   
�������
�������$��Modern No. 20)�6l   $
��Bold"�6�   $��48(�6�    �� IsCancel$
 True��6�    52      ��%_   '��       �� 53      " ��6    .�54      ��  �@��CornerRadius$ 25��6=   55      56       57        58      5:      5<      5@      5C      5D      