
MathUtil={}
local function ConvertToNumber(value, base)
    local val = tonumber(value, base)
    if val==nil then
        return false
        end
    return true,val
end
-- 对数值进行四舍五入，如果不是数值则返回 0
function MathUtil.Round(value)
    local bo,val = ConvertToNumber(value)
    if bo then
        return math.floor(val + 0.5)
    end
    return bo
end

function MathUtil.ToInt(value)
    local bo,val = ConvertToNumber(value)
    if bo then
        return MathUtil.Round(val)
        end
    return bo
end

function MathUtil.AngleToRadian(angle)
    return angle*math.pi/180
end
-- 弧度转角度
function MathUtil.RadianToAngle(radian)
    return radian/math.pi*180
end

return MathUtil