--*********************************************************************************
 --Author:         IFramework_Demo
 --Version:        0.0.1
 --UnityVersion:   2018.4.17f1
 --Date:           2020-06-06
 --Description:    Description
 --History:        2020-06-06--
--*********************************************************************************/

--super Fields 
----self.message : publish Event
----self.context : ViewModel
----self.panel :  UIpanel From C#
--super Function 
----self:PublishViewEvent(code,...)


local Panel01View=Class("Panel01View",require("UI.UIView"))

--Bind ViewModel Fields
function Panel01View:BindProperty()

end

function Panel01View:Dispose()

end

function Panel01View:OnLoad()
   
end

function Panel01View:OnTop( arg )

end

function Panel01View:OnPress( arg )

end

function Panel01View:OnPop( arg )

end

function Panel01View:OnClear()

end

return Panel01View
