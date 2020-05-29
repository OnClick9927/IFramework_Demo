--*********************************************************************************
 --Author:         IFramework_Demo
 --Version:        0.0.1
 --UnityVersion:   2018.4.17f1
 --Date:           2020-06-06
 --Description:    Description
 --History:        2020-06-06--
--*********************************************************************************/
--super Fields 
--super Function 
----self:Subscribe( key,func )
----self:UnSubscribe(key,func)
----self:Invoke( key )



local Panel01ViewModel=Class("Panel01ViewModel",require("UI.ViewModel"))

--return Panel01ViewModel's Fields By table
--Example return { myCount = 666 }
function Panel01ViewModel:GetFieldTable()

end

function Panel01ViewModel:OnDispose()

end

function Panel01ViewModel:OnInitialize()

end

--View's  Event 
function Panel01ViewModel:ListenViewEvent( code,... )

end

return Panel01ViewModel
