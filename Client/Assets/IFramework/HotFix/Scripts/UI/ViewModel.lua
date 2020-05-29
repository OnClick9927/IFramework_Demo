
local ViewModel = Class("ViewModel",ObservableObject)
function ViewModel:ctor( message )
 
    local t=self:GetFieldTable()

    ViewModel.super.ctor(self,t)
    self.message=message
    
    self.listenCallback=function (code,...)
        self.ListenViewEvent(code,...)
    end
end
function ViewModel:Initialize()
    self.message:Subscribe(self,self.listenCallback)
    self:OnInitialize()
end

function ViewModel:Dispose()
    self:OnDispose()
    self.message:UnSubscribe(self,self.listenCallback)
end


function ViewModel:GetFieldTable()
    
end

function ViewModel:OnDispose()

end

function ViewModel:OnInitialize()

end
function ViewModel:ListenViewEvent( code,... )

end
return ViewModel