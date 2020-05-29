
local VVMGroup = Class("VVMGroup")
function VVMGroup:ctor( panel,view,viewModel ,message )
    self.message=message
    self.viewModel=viewModel
    self.view=view
    self.panel=panel
    self.viewModel:Initialize()
end
function VVMGroup:Dispose()
    self.message:Dispose()

    self.viewModel:Dispose()
    self.view:Dispose()
end
return VVMGroup