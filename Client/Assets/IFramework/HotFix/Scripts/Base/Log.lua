local Log = {}
--public static int lev_L = 0;
--public static int lev_W = 0;
--public static int lev_E = 0;
--public static bool enable = true;
--public static bool enable_L = true;
--public static bool enable_W = true;
--public static bool enable_E = true;


function Log.L(lev,fmt,...)
    lev= tonumber(lev)
    local info = debug.getinfo(2,"S")
    local source=info.source
    info = debug.getinfo(2,"l")
    local message="LUA :"..string.format(fmt,...).."\nLine: "..info.currentline.."\n at  "..source
    CS.IFramework.Log.L(message,lev)
end
function Log.W(lev,fmt,...)
    lev= tonumber(lev)
    local info = debug.getinfo(2,"S")
    local source=info.source
    info = debug.getinfo(2,"l")
    local message="LUA :"..string.format(fmt,...).."\nLine: "..info.currentline.."\n at  "..source
    CS.IFramework.Log.W(message,lev)
end
function Log.E(lev,fmt,...)
    lev= tonumber(lev)
    local info = debug.getinfo(2,"S")
    local source=info.source
    info = debug.getinfo(2,"l")
    local message="LUA :"..string.format(fmt,...).."\nLine: "..info.currentline.."\n at  "..source
    CS.IFramework.Log.E(message,lev)
end




setmetatable(Log,{
    __index=function (t,k)
        if k=="lev_L" then
            return CS.IFramework.Log.lev_L
        elseif k=="lev_W" then
            return CS.IFramework.Log.lev_W
        elseif k=="lev_E" then
            return CS.IFramework.Log.lev_E
        elseif k=="enable" then
            return CS.IFramework.Log.enable
        elseif k=="enable_L" then
            return CS.IFramework.Log.enable_L
        elseif k=="enable_W" then
            return CS.IFramework.Log.enable_W
        elseif k=="enable_E" then
            return CS.IFramework.Log.enable_E
        else
            error("No such Field in Log :"..tostring(k))
        end
    end
,
    __newindex=function(t,k,v)
        if k=="lev_L" then
            CS.IFramework.Log.lev_L =v
        elseif k=="lev_W" then
            CS.IFramework.Log.lev_W =vs
        elseif k=="lev_E" then
            CS.IFramework.Log.lev_E =v
        elseif k=="enable" then
            CS.IFramework.Log.enable =v
        elseif k=="enable_L" then
            CS.IFramework.Log.enable_L =v
        elseif k=="enable_W" then
            CS.IFramework.Log.enable_W =v
        elseif k=="enable_E" then
            CS.IFramework.Log.enable_E =v
        else
            error("No such Field in Log :"..tostring(k))
        end
    end
})

return Log