   M S B A M L   `   `   `  ����@  <Lab_9, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab_9Lab_9  5   	       Lab_9.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Lab_9     5   	     � Title$W  QИсследование криптографических хеш-функций��5   	   $	��450��6<   $	��800��6I   .��5	      �  � RowDefinitions 5
   
   �� 5      $��1* �6   6*   �� 5      $	��18* �6   6+   �� 5      $��1* �6   6*   �� 5      $��2* �6   6*   �� 5      $��1* �6   6*    � ColumnDefinitions 5   
   �� 5      $��1* �6   6,   �� 5      $	��20* �6   6-   �� 5      $��1* �6   6,   �� 5      $	��20* �6   6-   �� 5      $��1* �6   6,   .+�5   
   �� -   5        $��RichTextOrig��5      $��
5, 5, 5, 5q�6*   $��15(�6>   $��1��6L   $��1��6Y   6k   �� 5   
   -   5        $��RichTextHash��5      $��
5, 5, 5, 5q�6*   $��15(�6>   $��1��6L   $��3��6Y    �� 
IsReadOnly . 6}   �� 5   
   -   5        $��Button��5      $��
5, 5, 5, 5q�6   $��15(�63   $��3��6A   $��1��6N   $��3��6^   .��6   Hash6�   5      5      