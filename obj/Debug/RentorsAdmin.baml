   M S B A M L   `   `   `  ����L  HPavilions_program, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Pavilions_programPavilions_program  5   	   $    Pavilions_program.RentorsAdmin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Pavilions_program     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ RentorsAdmin��5	   	   $	��450��6   $	��800��6+   .��5
      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$
 grid��5   %      
IsReadOnly . $
��Left=�63   $	��318��6N   $��	60,48,0,0q�6[   $	��Top=�6n   $	��680��6�   �� 5   
   -   5         ��Name$
 back��5      $��
Назад��6   $
��Left=�6/   $��
60,371,0,0q�6J   $	��Top=�6^   $	��165��6v   $��20��6�   6�   �� 5   
   -   5        $��Удалить��5      $
��Left=�6#   $��232,371,0,0q�6>   $	��Top=�6S   $	��165��6k   $��20��6w   6�   �� 5   
   -   5        $ ToAddRenter��5      $��Добавить��6&   $
��Left=�69   $��403,371,0,0q�6T   $	��Top=�6i   $	��165��6�   $��20��6�   6�   �� 5   
   -   5        $ ToEditRenter��5      $ ��Редактировать��6'   $
��Left=�6?   $��575,371,0,0q�6Z   $	��Top=�6o   $	��165��6�   $��20��6�   6�   �� 5   
   -   5         ��Name$ search_function��5      $
��Left=�6+   $��20��6F   $��
185,23,0,0q�6R    �� TextWrapping$
 Wrap=�6f   $	��Top=�6z   $	��418��6�   6�   �� 5   
   $'��!Поиск по названию:��6   $
��Left=�6-   $��	60,20,0,0q�6H   $	��Top=�6[   $	��125��6s   $��23��6   �� 5   
   -   5        $ clear��5      $��Очистить��6    $
��Left=�63   $��
608,23,0,0q�6N   $	��Top=�6b   $	��132��6z   $��20��6�   6�   5      5      