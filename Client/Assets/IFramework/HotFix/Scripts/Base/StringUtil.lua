local StringUtil={}

-- 用指定字符或字符串分割输入字符串，返回包含分割结果的数组

-- local input = "Hello,World"
-- local res = string.split(input, ",")
-- res = {"Hello", "World"}

-- local input = "Hello-+-World-+-Quick"
-- local res = string.split(input, "-+-")
-- res = {"Hello", "World", "Quick"}
function StringUtil.Split(input, delimiter)
    input = tostring(input)
    delimiter = tostring(delimiter)
    if (delimiter=='') then return false end
    local pos,arr = 0, {}
    -- for each divider found
    for st,sp in function() return string.find(input, delimiter, pos, true) end do
        table.insert(arr, string.sub(input, pos, st - 1))
        pos = sp + 1
    end
    table.insert(arr, string.sub(input, pos))
    return arr
end

function StringUtil.TrimHead(input)
    return string.gsub(input, "^[ \t\n\r]+", "")
end

-- start --

--------------------------------
-- 去除输入字符串尾部的空白字符，返回结果
function StringUtil.TrimTail(input)
    return string.gsub(input, "[ \t\n\r]+$", "")
end

function StringUtil.Trim(input)
    input = string.gsub(input, "^[ \t\n\r]+", "")
    return string.gsub(input, "[ \t\n\r]+$", "")
end
-- 将字符串的第一个字符转为大写，返回结果
function StringUtil.UpperFirst(input)
    return string.upper(string.sub(input, 1, 1)) .. string.sub(input, 2)
end
-- 计算 UTF8 字符串的长度，每一个中文算一个字符
-- local input = "你好World"
-- PTPrint(string.utf8len(input))
-- 输出 7
function StringUtil.UTF8Length(input)
    local len  = string.len(input)
    local left = len
    local cnt  = 0
    local arr  = {0, 0xc0, 0xe0, 0xf0, 0xf8, 0xfc}
    while left ~= 0 do
        local tmp = string.byte(input, -left)
        local i   = #arr
        while arr[i] do
            if tmp >= arr[i] then
                left = left - i
                break
            end
            i = i - 1
        end
        cnt = cnt + 1
    end
    return cnt
end

-- start --

--------------------------------
-- 将数值格式化为包含千分位分隔符的字符串

-- PTPrint(string.formatnumberthousands(1924235))
-- 输出 1,924,235


function StringUtil.FormatNumberThousands(num)
    local bo, formatted= math.ConvertToNumber(num)
    if not bo then
        return nil
        end
    formatted = tostring(formatted)
    local k
    while true do
        formatted, k = string.gsub(formatted, "^(-?%d+)(%d%d%d)", '%1,%2')
        if k == 0 then break end
    end
    return formatted
end
return StringUtil