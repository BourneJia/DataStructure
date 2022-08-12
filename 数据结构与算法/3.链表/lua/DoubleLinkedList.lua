local DoubleListNode = {
  mData = {},
  mPrevious = nil,
  mNext = nil
}

function DoubleListNode:New()
  local o = {}
  setmetatable(o,self)
  self.__index = self
  o.mData = self.mData
  o.mPrevious = self.mPrevious
  o.mNext = self.mNext

  return o
end

local DoubleLinedList = {
  mFirst = DoubleListNode:New(),
  mLast = DoubleListNode:New(),
  mLength = 0
}

function DoubleLinedList:New()
  local o = {}
  setmetatable(o,self)
  self.__index = self

  return o
end

function DoubleLinedList:Prepend(value)
  local newNode = DoubleListNode:New()
  newNode.mData = value

  if(self.mLength == 0)
  then
    self.mFirst = newNode
    self.mLast = newNode
  else
    local currentNode = self.mFirst
    self.mFirst = newNode
    newNode.mNext = currentNode
    currentNode.mPrevious = newNode
  end

  self.mLength = self.mLength + 1
end

function DoubleLinedList:Append(value)
  local newNode = DoubleListNode:New()
  newNode.mData = value

  if(self.mLength == 0)
  then
    self.mFirst = newNode
    self.mLast = newNode
  else
    local currentNode = self.mLast
    self.mLast = newNode
    currentNode.mNext = newNode
    newNode.mPrevious = currentNode
  end

  self.mLength = self.mLength + 1
end

function DoubleLinedList:Insert(index, value)
  assert(type(index)=="number","index is not a number")
  assert(index > 0 and index <= self.mLength+1,"Index was outside the bounds of the list")

  if(index == 1)
  then
    self:Prepend(value)
  elseif(index == self.mLength+1) 
  then
    self:Append(value)
  else
    local currentNode = DoubleListNode:New()
    local newNode = DoubleListNode:New()
    newNode.mData = value
    if(index > self.mLength/2)
    then
      currentNode = self.mLast
      local i = self.mLength
      while(i>index)
      do
        currentNode = currentNode.mPrevious
        i = i-1
      end
      --local oldNode = currentNode

      if(currentNode ~= nil)
      then
        currentNode.mPrevious.mNext = newNode
      end

      newNode.mNext = currentNode
      --currentNode.mNext = newNode
      newNode.mPrevious = currentNode.mPrevious

      self.mLength = self.mLength + 1
    else
      currentNode = self.mFirst
      local i = 1
      while(i < index)
      do
        currentNode = currentNode.mNext
        i = i+1
      end

      --local oldNode = currentNode

      if(currentNode ~= nil)
      then
        currentNode.mPrevious.mNext = newNode
      end

      newNode.mNext = currentNode
      --currentNode.mNext = newNode
      newNode.mPrevious = currentNode.mPrevious

      self.mLength = self.mLength + 1
    end
  end

end

function DoubleLinedList:Delete(index)
  assert(type(index)=="number","index is not a number")
  assert(index > 0 and index <= self.mLength,"Index was outside the bounds of the list")
  assert(self.mLength ~= 0,"Index was outside the bounds of the list")

  if(index == 1) then
    self.mFirst = self.mFirst.mNext
    if(self.mFirst ~= nil) then
      self.mFirst.mPrevious = nil
    end
  elseif (index == self.mLength) then
    self.mLast = self.mLast.mPrevious
    if(self.mLast ~= nil) then
      self.mLast.mNext = nil
    end
  elseif (index > self.mLength/2) then
    local currentNode = self.mLast
    local i = self.mLength
    while(i > index)
    do
      currentNode = currentNode.mPrevious
      i = i-1
    end
    local newPre = currentNode.mPrevious
    local newNext = currentNode.mNext
    newPre.mNext = newNext

    if(newNext ~= nil) then
      newNext.mPrevious = newPre
    end
  else
    local currentNode = self.mFirst
    local i = 1
    while(i < index)
    do
      currentNode = currentNode.mNext
    end
    local newPre = currentNode.mPrevious
    local newNext = currentNode.mNext
    newNext.mPrevious = newPre
    if(newPre~=nil) then
      newPre.mNext = newNext
    end
  end
  self.mLength = self.mLength - 1
end

function DoubleLinedList:Find(index)
  assert(type(index)=="number","index is not a number")
  assert(index > 0 and index <= self.mLength,"Index was outside the bounds of the list")
  assert(self.mLength ~= 0,"Index was outside the bounds of the list")

  if(index==1) then
    return self.mFirst.mData
  elseif(index == self.mLength) then
    return self.mLast.mData
  end

  local currentNode = DoubleListNode:New()

  if(index > self.mLength/2) then
    currentNode = self.mLast
    local i = self.mLength
    while(i > index)
    do
      currentNode = currentNode.mPrevious
      i = i-1
    end
  else
    currentNode = self.mFirst
    local i = 1
    while(i < index)
    do
      currentNode = currentNode.mNext
      i = i+1
    end
  end

  return currentNode.mData
end

local a = DoubleLinedList:New()

-- a:Prepend(5)
-- a:Prepend(3)
-- a:Prepend(4)
-- a:Prepend(15)
-- a:Prepend(17)
-- a:Append(12)

a:Insert(1,12)
a:Insert(2,14)
a:Insert(3,16)
a:Insert(4,17)
a:Insert(5,18)
a:Insert(6,23)
a:Insert(7,25)
a:Insert(8,124)
a:Insert(9,126)
a:Insert(10,129)
a:Insert(11,122)

a:Insert(3,2124)

-- a:Delete(4)
-- a:Delete(3)
--a:Delete(6)

local currentNode = a.mFirst
while(currentNode ~= nil)
do
  print(currentNode.mData)
  currentNode = currentNode.mNext
end

-- print(a.mFirst.mData)
-- print(a.mFirst.mNext.mData)
-- print(a.mFirst.mNext.mNext.mData)

-- for i,v in ipairs(a) do
--   print(i,v)
-- end

-- print(a:Find(1))
-- print(a:Find(2))
-- print(a:Find(3))
-- print(a:Find(4))
-- print(a:Find(5))
--print(a:Find(6))
-- a:Insert(1,3)
-- print(a:Find(1))
-- print(a:Find(2))
-- print(a:Find(3))

print(a.mLength)