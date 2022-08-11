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

  if(index == 0)
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
      while(i>index+1)
      do
        currentNode = currentNode.mPrevious
        i = i-1
      end
      local oldNode = currentNode

      if(oldNode ~= nil)
      then
        currentNode.mNext.mPrevious = newNode
      end

      newNode.mNext = oldNode
      currentNode.mNext = newNode
      newNode.mPrevious = currentNode

      self.mLength = self.mLength + 1
    else
      currentNode = self.mFirst
      local i = 1
      while(i < index+1)
      do
        currentNode = currentNode.mNext
        i = i+1
      end

      local oldNode = currentNode

      if(oldNode ~= nil)
      then
        currentNode.mNext.mPrevious = newNode
      end

      newNode.mNext = oldNode
      currentNode.mNext = newNode
      newNode.mPrevious = currentNode

      self.mLength = self.mLength + 1
    end
  end

end

function DoubleLinedList:Delete(index)
  assert(type(index)=="number","index is not a number")
  assert(index > 0 and index <= self.mLength,"Index was outside the bounds of the list")
  assert(self.mLength == 0,"Index was outside the bounds of the list")

  if(index == 0) then
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
    while(i>=index)
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
    while(i <= index)
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
  assert(self.mLength == 0,"Index was outside the bounds of the list")

  if(index==0) then
    return self.mFirst.mData
  elseif(index == self.mLength) then
    return self.mLast.mData
  end

  local currentNode = DoubleListNode:New()

  if(index > self.mLength/2) then
    currentNode = self.mLast
    local i = self.mLength
    while(i >= index)
    do
      currentNode = currentNode.mPrevious
      i = i-1
    end
  else
    currentNode = self.mFirst
    local i = 1
    while(i <= index)
    do
      currentNode = currentNode.mNext
      i = i+1
    end
  end

  return currentNode.mData
end


print(123)