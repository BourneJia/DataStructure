--lua中..表示两个字符串的相加

local s = ""
for i = 1, 10 do
  s = s .. tostring(i)  
end
print(s)

--lua中，字符串有三种方式表示：单引号，双引号，以及长括号([[]])。其中长括号中的字符串不会做任何的转义处理
print([[string has \n and \r]])
print("string has \n and \r")
print('string has \n and \r')

--如果字符串包含长括号本身，该怎么处理呢？在长括号中间增加一个或者多个 = 符号
print([=[ string has a [[]]. ]=])