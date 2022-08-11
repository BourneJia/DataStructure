local Shape = {
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
  print(self.area)
end

-- ShapeB = {
--   area = 12345
-- }

-- mtShapeB = { __index = mtShapeB }

-- function ShapeB:new(side)
--   --self.area = side*side
--   local o = {}
--   return setmetatable(o, mtShapeB )
--   --mtShapeB.__index = mtShapeB
--   --return o
-- end

-- function ShapeB:PrintArea()
--   print("size:",self.area)
-- end

local x = Shape:new(nil,2)
local y = Shape:new(nil,3)
x:PrintArea()
y:PrintArea()
Shape.area = 222
x:PrintArea()
y:PrintArea()