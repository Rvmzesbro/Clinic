   M S B A M L   `   `   `  ����A  =Clinic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Clinic.PagesClinic.Pages  5          Clinic.Pages.Main   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Clinic.Pages     5      -   5          N� Title$
  Main��5	       N� 
Background �	#FFE6E6E6.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   (� $��1��6   $��5q�6    .��5      �� -   5        $��MainTabControl��5   1   $��1��6   $��5q�6&   .@�5      �� -   5         ��Name$ 	TIDoctors��5      $��
Врачи��6-   .��5      &   System.Windows.Controls.DataGrid  -   5        $��DoctorsGrid��5         AutoGenerateColumns .     CanUserAddRows .    CanUserDeleteRows .    CanUserReorderColumns .    
IsReadOnly . 	   SelectionMode$	 Single=�5      ���#9370DB
���   �$��1q�5       ��5      �� 
   Columns
 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��61      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *Id+6T     5$      $ Фамилия��61    6B   �� *	Surname+  5%      $ Имя��61    6>   �� *Name+  5&      $ Отчество��61    6C   �� *
Patronymic+  5'      $ Пол��61    6>   �� *Genders.Name+  5(      $ Телефон��61    6B   �� *Phone+  5)      $ Зарплата��61    6C   �� *Salary+  5*      $  Специальность��61    6H   �� *Specialitys.Name+5,      5-      �� 5/      -   5        $ 
TIPatients��50   
   $��Пациенты��5/      .��51        -   5        $��PatientsGrid��51       .   .  .  .  . $	 Single=�54   '   ��6   �� 
 55        56      $ ID��61    6=   �� *Id+  57      $ Фамилия��61    6B   �� *	Surname+  58      $ Имя��61    6>   �� *Name+  59      $ Отчество��61    6C   �� *
Patronymic+  5:      $ Пол��61    6>   �� *Genders.Name+  5;      $ Телефон��61    6B   �� *Phone+  5<      $ День рождение��61    6H   �� *DateBirthday+ �� StringFormat$ 
dd.MM.yyyy��  5=      $ Возраст��61    .  6B   �� *Age+6Z     5>      $ Участок��61    6B   �� *
Plots.Name+5@      5A      �� 5C      -   5        $ TIReception��5D   
   $��Записи��5C      .��5E        -   5        $��ReceptionGrid��5E       .   .  .  .  . $	 Single=�5H   '   ��6   �� 
 5I        5J      $ ID��61    .  6=   �� *Id+6T     5K      $ Врач��61    .  6?   �� *Doctors.FullName+6d     5L      $ Пациент��61    .  6B   �� *Patients.FullName+6h     5M      $ Время записи��61    5N      �� *
TimeNote+$ dd.MM.yyyy HH:mm��5P      5Q      �� 5S      -	   5        $ TISchedules��5T   
   $'��!Расписание врачей��5S      .��5U        -
   5        $��ShedulesGrid��5U       .   .  .  .  . $	 Single=�5X   '   ��6   �� 
 5Y        5Z      $ ID��61    .  6=   �� *Id+6T     5[      $ Врач��61    .  6?   �� *Doctors.FullName+6d     5\      $ Начало приема��61    6H   �� *BeginingDate+$ hh\:mm��  5]      $% Окончание приема��61    6K   �� *	EndDate+$ hh\:mm��5_      5`      5a      5b      �� 5f   
   $��0��6   $��
Поиск��5g      $��5q�5h      
���   �.��5j      ��  �� Orientation$ 
Horizontal=�6   .+�5k      �� -   5        $��SearchTextBox��5k      $	��200��6/   $��5q�6;   5m      �� 5n      -   5         ��Name$ CBFilter��5n      $	��200��5p      $��25��5q      5s      �� 5t      -   5         ��Name$ BTClear��5t      $��5q�6@   $��80��5u      $��20��5v      .��5w      �� $��Очистить��6    $��12(�5x       5y      5z      5{      �� 5}   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   $��5q�6W   .+�5~      �� -   5        $ BTAdd��5~      $��Добавить��5      $��5q�5�      $	��100��5�      $��30��5�      �� 5�      -   5        $ BTEdit��5�      $��Изменить��5�      $��5q�5�      $	��100��5�      $��30��5�      �� 5�      -   5        $ BTDelete��5�      $��Удалить��5�      $��5q�5�      $	��100��5�      $��30��5�      5�      5�      5�      