
local MathUtil={}
---ConvertToNumber 对象-》数值
---@param value  object
---@param base  进制
local function ConvertToNumber(value, base)
    local val = tonumber(value, base)
    if val==nil then
        return false
        end
    return true,val
end

---Round 对数值进行四舍五入，如果不是数值则返回 0
---@param value 数值
function MathUtil.Round(value)
    local bo,val = ConvertToNumber(value)
    if bo then
        return math.floor(val + 0.5)
    end
    return bo
end

---ToInt 转化为 int32
---@param value 数值
function MathUtil.ToInt(value)
    local bo,val = ConvertToNumber(value)
    if bo then
        return MathUtil.Round(val)
        end
    return bo
end

---AngleToRadian 角度-》弧度
---@param angle 角度 
function MathUtil.AngleToRadian(angle)
    return angle*math.pi/180
end

---RadianToAngle 弧度转角度
---@param radian 弧度
function MathUtil.RadianToAngle(radian)
    return radian/math.pi*180
end

return MathUtil