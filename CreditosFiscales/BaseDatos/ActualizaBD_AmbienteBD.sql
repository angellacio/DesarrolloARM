select * from ApplicationSettings

update ApplicationSettings 
set SettingValue = 'Password=SQLadmin2020;Persist Security Info=True;User ID=sa;Initial Catalog=CreditosFiscales;Data Source=35.86.96.4' 
where SettingName = 'Log:ConnStrCreditosFiscales'

update ApplicationSettings 
set SettingValue = 'Password=SQLadmin2020;Persist Security Info=True;User ID=sa;Initial Catalog=MotorTraductor;Data Source=35.86.96.4' 
where SettingName = 'Log:ConnStrMotorTraductor'


