Shape = {
  area = 0
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

  return setmetatable({side = side}, { __index = self})
end

function ShapeB:PrintArea()
  print("size:",self.area)
end


local a = Shape:new(nil,2)
print("a:")
print(a.area)
print(a.__index)
a:PrintArea()

local b = ShapeA:new(3)
print("b:")
print(b.area)
print(b.__index)
b:PrintArea()

local c = ShapeB:new(4)
print("c:")
print(c.area)
--print(c.side)
print(c.__index)
c:PrintArea()
--print(c:__index())


--继承
local d =ShapeA:new(5)
print("d:")
print(d.area)
d.area = 30
print("d:")
print(d.area)
print("b:")
print(b.area)

local e =ShapeB:new(6)
print("e:")
print(e.area)
e.area = 30
print("e:")
print(e.area)
print("c:")
print(c.area)