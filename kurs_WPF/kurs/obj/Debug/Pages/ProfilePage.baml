   M S B A M L   `   `   `  ����?  ;kurs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:kurs.Pages
kurs.Pages  5          kurs.Pages.ProfilePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:kurs.Pages     5      -   5          N� Title$  ProfilePage��5	      .��5      �  � ColumnDefinitions 5   
   ��5   
   �� 5       �� ImageSource$ \Pictures\blure 3.jpg��6    j� Stretch$
 Fill=�6>   .+�5   
   �� 
���   � J� Opacity$	 0.9Q�6)   $	��700��67   $	��450��6C   $��2��6P   $��50,0,0,0q�6d   $��2��6v   .+�5      �� -   5         ��Name$ BtnBack��5      $��
Назад��6&   $��	15,22,0,5q�6L   $	��100��6_      btn#��[  6k   $��30��6�   $	��Top=�6�   $
��Left=�6�    � 	Resources 5       �� 
TargetType �� Property	 �� Value��%_   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$	 10��69   5      5      5      5      �� 5   
   $��Center=�6   $	��450��62   $��	0,0,300,0q�6?   .+�5      �� $	��332��6   $��
70,50,0,20q�6&   
 �� Orientation$
 
Horizontal=�6:   $��Center=�6S   .+�5      �� $��Логин:��6   $��54��6+   $��	10,15,0,5q�66     BlockCommon#��[ 6I   �� 5      -   5        $��TBoxProfileLogin��5      $	��155��62   $��
25,15,30,5q�6>   
�������
�������.��5   A   ��5      �� 5        �� Color$ Transparent��6*    �� Opacity$	 0.3Q�6>   .��5!   *   5"      5#      �� 5$      $	��332��6   $��	70,0,0,20q�6&   $
 
Horizontal=�69   $��Center=�6R   .+�5%      �� $��Имя :��6   $��54��6*   $��	10,15,0,5q�65   #��[ 6H   �� 5&      -   5        $��TBoxProfileName��5&      $	��155��61    �� 
IsReadOnly . $��
25,15,30,5q�6O   
������� �� SelectionTextBrush
 �����.��6�   ��5'      �� 5(      $ White��6*   $ 1Q�68   .��5)   *   ��5*      �� 5+      $ 0Q�6*   .��5,   *   5-      5.      �� 5/      $	��332��6   $��	70,0,0,20q�6&   $
 
Horizontal=�69   $��Center=�6R   .+�50      �� $��Фамилия:��6   $��54��6-   $��	10,15,0,5q�68   #��[ 6K   �� 51      -   5        $��TBoxProfileSurname��51       . $	��155��6F   $��
25,15,30,5q�6R   
�������
�������
 �����.��6�   ��52      �� 53      $ Transparent��6*   $	 0.3Q�6>   .��54   *   55      56      �� 57      $	��332��6   $��70,0,0,0q�6&   $
 
Horizontal=�68   $��Center=�6Q   .+�58      �� $��Пароль:��6   $��54��6,   $��	10,15,0,5q�67   #��[ 6J   �� 59      -   5        $��TBoxProfilePassword��59       . $	��155��6H   $��
25,15,10,5q�6T   
�������
�������
 �����.��6�   ��5:      �� 5;      $	 0.3Q�6*   $ Transparent��68   .��5<   *   5=      �� 5>      $��\Pictures\infa.png��6   $��20��64   $��Center=�6?   $��	0,10,10,0q�6Z   $��23��6m    � ToolTip 5?      _� 5@      $����Пароль должен содержать:
*От 8 до 16 символов
*Минимум 1 заглавная буква 
*Минимум 1 спецсимвол 
��6"   $
��auto��5A   "   $
��auto��60   ���#332F2C
�������$
 0.70Q�6e   5C      5D      �� 5E      $
 
Horizontal=�6   $��Center=�62   .+�5F      �� -   5        $��TBlockPasswordCheck��5F      $(��"Надежность пароля:��67   $	��144��6Q   $��
135,5,70,0q�6]   $
 0.70Q�6q   #��[ 6�   5G      �� 5H      .+�5I      �� -   5         ��Name$ TBoxPasswordCheck��5I      $	��155��65   {�.  ���	#00000000���	#FF969696
�������$
��Left=�6�   $��	160,0,0,0q�6�   5J      �� 5K      .+�5L      �� -	   5        $��BtnProfileDelete��5L   �   #��[  6   $��30��66   $	��200��6B   $y�Hidden=�6N   $��	70,30,5,5q�6b   $
��Left=�6u   $#��Удалить профиль��5M   <    5N      ��%_   '��       �� 5O      " ��6    .�5P      ��  �@��CornerRadius$	 10��6=   5Q      5R      5S      5T      �� 5U      -
   5        $��BtnProfileEdit��5U   x   #��[  6   $��30��62   $	��200��6>   $��70,8,5,5q�6J   $
��Left=�6\   $ ��Редактировать��5V   :    5W      ��%_   '��       �� 5X      " ��6   .�5Y      ��  �@��CornerRadius$	 10��69   5Z      5[      5\      5]      �� 5^   
   $	��400��6   $��
500,5,50,0q�6"   $��Center=�66   .+�5_      �� $��5q�6   $	��175��6    $	��200��6-   
�������$��1q�6L   ._�5`      �� -   5        $��ImageAvatar��5`      5a      �� 5b      -   5        $��BtnSelectImage��5b   o   #��[  6   $��30��62   $	��200��6>   $y�Hidden=�6J   $��0,0,5,5q�6^   $)��#Выбрать фотографию��5c   :    5d      ��%_   '��       �� 5e      " ��6   .�5f      ��  �@��CornerRadius$	 10��69   5g      5h      5i      �� 5j      -   5        $��BtnSave��5j   �   #��[  6   $��30��63   $	��200��6?   $y�Hidden=�6K   $��	5,135,5,5q�6_   $��Bottom=�6r   $��Сохранить��5k   3    5l      ��%_   '��       �� 5m      " ��6   .�5n      ��  �@��CornerRadius$	 10��69   5o      5p      5q      5r      5s      5t      