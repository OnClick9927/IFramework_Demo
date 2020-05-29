local UIView = Class("UIView")
function UIView:ctor( message,context,panel )
    self.message=message
    self.context=context
    self.panel=panel
    self:BindProperty()
end
function UIView:Show( )
    self.panel.gameObject:SetActive(true)
end
function UIView:Hide( )
    self.panel.gameObject:SetActive(false)
end
function UIView:PublishViewEvent(code,...)
    self.message:Invoke(code,...)
end


function UIView:BindProperty()

end

function UIView:Dispose()

end


function UIView:OnLoad(  )

end
function UIView:OnTop( arg )

end
function UIView:OnPress( arg )

end
function UIView:OnPop( arg )

end
function UIView:OnClear(  )

end

return UIView