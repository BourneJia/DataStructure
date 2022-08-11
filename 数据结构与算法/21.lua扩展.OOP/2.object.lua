Shape = {
  area = 12345
}

function Shape:new(o, side)
  o = o or {}
  setmetatable(o, self)
  self.__index = self
  side = side or 0
  self.area = side*side;
  return o
end

function Shape:PrintArea()
  print("size:", self.area)
end

local z = Shape:new(nil,1)

local n = z:new(nil,2)
z.area = 10
print(z.area)
print(n.area)

ShapeA = {
  area = 0
}

function ShapeA:new(side)
  self.area = side*side

  return self
end

function ShapeA:PrintArea()
  print("size:",self.area)
end

local ShapeB = {
  area = 0
}

local mtShapeB = { __index = ShapeB }

function ShapeB:new(side)
  self.area = side*side

  return setmetatable({side = side}, mtShapeB )
end

function ShapeB:PrintArea()
  print("size:",self.area)
end


local a = Shape:new(nil,2)
print("a:"..a.area)
-- print(a.area)
print(a.__index)
a:PrintArea()

local b = ShapeA:new(3)
print("b:"..b.area)
--print(b.area)
print("b.__index:  "..tostring(b.__index))
b:PrintArea()

local c = ShapeB:new(4)
print("c:"..c.area)
--print(c.area)
print("c.__index: "..tostring(c.__index))
c:PrintArea()

--遍历下内容a 和 c 看看为啥index指向有区别
print("look a for loop")
for i,v in ipairs(a) do
  print(v)
end

print("look c for loop")
for i,v in ipairs(c) do
  print(tostring(v))  
end

--继承
local x = Shape:new(nil,8)
print("x:"..x.area)
x.area = 70
print("x:"..x.area)
print("a:"..a.area)

local d =ShapeA:new(5)
print("d:"..d.area)
d.area = 30
print("d:"..d.area)
print("b:"..b.area)

local e =ShapeB:new(6)
print("e:"..e.area)
e.area = 30
print("e:"..e.area)
print("c:"..c.area)