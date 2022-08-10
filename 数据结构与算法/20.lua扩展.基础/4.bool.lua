--lua中, 只有nil和false为假，其它都为真，包括0和空字符也为真。
local a = 0

if a then
  print("true")
end

a = ""
if a then
  print("true")
end

--为了避免在上述问题中出错，因此可以显式地写明比较的对象
local a = 0
if a == false then
  print("true")
end