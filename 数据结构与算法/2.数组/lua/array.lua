local ArrayList = {
  mCap = 0,
  mData = {},
  mLength = 0,
}

function ArrayList:New(capacity)
  o = {}
  self.mCap = capacity
  self.mData = {}
  self.mLength = 0

  setmetatable(o,{ __index = self })
  return o
end

function ArrayList:Insert(index, data)
  assert(type(index) == "number", "index is not a int")
  -- assert(self.mCap == self.mData, "数组没有多余空间进行添加") 还是去扩容算了
  assert(index >= 1 and index <= self.mLength+1, "Index was outside the bounds of the list")
  if(self.mCap == self.mLength)
  then
    self:resize()
  end
  -- lua 的for循环感觉不太适合条件遍历呀......有点难把控
  -- for i = self.mLength+1, self.mLength-index+2, -1 do
  --   print("self.mData["..i.."]:"..tostring(self.mData[i]))
  --   print("self.mData["..(i+1).."]:"..tostring(self.mData[i+1]))
  --   self.mData[i] = self.mData[i-1]
  -- end
  --lua中的下标从1开始.......
  local i = self.mLength+1
  while(i > index)
  do  
    self.mData[i] = self.mData[i-1]
    i = i - 1
  end

  self.mData[index] = data
  self.mLength = self.mLength + 1

  return true
end

function ArrayList:Find(index)
  assert(type(index) == "number", "index is not a number")
  assert(index >= 1 and index < self.mLength+1, "Index was outside the bounds of the list")

  -- result["result"] = true
  -- result["data"] = self.mData[index]
  print(self.mData[index])

  return true
end

function ArrayList:Delete(index)
  assert(type(index) == "number", "index is not a int")
  assert(index >= 1 and index <= self.mLength+1, "Index was outside the bounds of the list")

  data = self.mData[index]
  while(self.mData[index] ~= nil)
  do
    self.mData[index] = self.mData[index+1]
    index = index+1
  end

  return true
end

function ArrayList:resize()
  self.mCap = self.mCap * 2
end


-- 测试
local a = ArrayList:New(8)
-- local b =  ArrayList:New(16)
--初始容量
print(a.mCap)
--插入10个数字，且容量进行扩充
a:Insert(1,2)
a:Insert(2,3)
a:Insert(3,4)
a:Insert(4,5)
a:Insert(5,6)
a:Insert(6,7)
a:Insert(7,8)
a:Insert(8,9)
a:Insert(9,10)
a:Insert(10,11)

--查找第三个数 输出4
a:Find(3)

--删除第三个数
a:Delete(3)

--原本第四个数替换成第三个数 输出5
a:Find(3)

--插入第三位数
a:Insert(3,50)

--再次查找第三位数，显示50
a:Find(3)
--查找第四位，之前的第三位再次变为第四位
a:Find(4)
--查找第10位，输出11
a:Find(10)

--进行了扩容，当前容量为16
print(a.mCap)

print("index of a list:")
for i,v in ipairs(a.mData) do
  print(v)  
end

-- print("index of b list:")
-- for i,v in ipairs(b.mData) do
--   print(v)  
-- end