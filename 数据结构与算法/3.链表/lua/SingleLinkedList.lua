local SingleLinkedNode = {
  mData = {},
  mNext = nil,
  mLength = 0
}

function SingleLinkedNode:New()
  local o = {}
  setmetatable(o,self)
  self.__index = self
  o.mData = self.mData
  o.mNext = self.mNext
  o.mLength = self.mLength
  return o
  --setmetatable(o,self)
end

local SingleLinkedList = {
  mFirst = SingleLinkedNode:New(),
  mLast = SingleLinkedNode:New(),
  mLength = 0
}

function SingleLinkedList:New()
  local o = {}
  setmetatable(o,self)
  self.__index = self
  -- o.mFirst = self.mFirst
  -- o.mLast  = self.mLast
  -- o.Length = self.mLength
  return o
end

function SingleLinkedList:Prepend(value)
  local newNode = SingleLinkedNode:New()
  newNode.mData = value
  if(self.mLength == 0)
  then
    self.mFirst = newNode
    self.mLast = newNode
  else
    local currentNode = self.mFirst
    newNode.mNext = currentNode
    self.mFirst = newNode
  end

  self.mLength = self.mLength+1
end

function SingleLinkedList:Append(value)
  local newNode = SingleLinkedNode:New()
  newNode.mData = value
  if(self.mLength == 0)
  then
    self.mFirst = newNode
    self.mLast  = newNode
  else
    local currentNode = self.mLast
    currentNode.mNext = newNode
    self.mLast = newNode
  end

  self.mLength = self.mLength+1
end

function SingleLinkedList:Insert(index, value)
  assert(type(index)=="number","index is not a number")
  assert(index > 0 and index <= self.mLength+1,"Index was outside the bounds of the list")

  if(index == 1)
  then
    self:Prepend(value)
  elseif(index == self.mLength+1)
  then
    self:Append(value)
  else
    local currentNode = self.mFirst
    local newNode = SingleLinkedNode:New()
    newNode.mData = value
    local i = 1
    while(i < index)
    do
      currentNode = currentNode.mNext
      i = i+1
    end
    newNode.mNext = currentNode.mNext
    currentNode.mNext = newNode
    self.mLength = self.mLength + 1
  end

  return true
end

function SingleLinkedList:Delete(index)
  assert(type(index)=="number","index is not a number")
  assert(index > 0 and index <= self.mLength,"Index was outside the bounds of the list")

  if(index==1)
  then
    self.mFirst = self.mFirst.mNext
    self.mLength = self.mLength-1 
  elseif (index==self.mLength) 
  then
    local currentNode = SingleLinkedNode:New()
    currentNode = self.mFirst

    while(currentNode.mNext ~= nil and currentNode.mNext ~= self.mLast)
    do
      currentNode = currentNode.mNext
    end
    
    currentNode.mNext = nil
    self.mLast = currentNode
  else
    local i = 1
    local currentNode = self.mFirst

    while(i < index)
    do
      if(i == index-1)
      then
        currentNode.mNext = currentNode.mNext.mNext
        self.mLength = self.mLength - 1
        break
      end
      currentNode = currentNode.mNext
      i = i+1
    end
  end

  return true
end

function SingleLinkedList:Find(index)
  assert(type(index)=="number","index is not a number")
  assert(index > 0 and index <= self.mLength,"Index was outside the bounds of the list")

  local currentNode = self.mFirst
  --currentNode = self.mFirst

  if(index == 1 )
  then
    currentNode = self.mFirst
  elseif(index == self.mLength)
  then
    currentNode = self.mLast
  else
    local i = 1
    while(i < index)
    do
      currentNode = currentNode.mNext
      i = i+1
    end
  end

  return currentNode
end

local a = SingleLinkedList:New()

a:Prepend(3)
a:Append(6)
print(a:Find(1).mData)
a:Insert(1,5)
print(a:Find(1).mData)
print(a:Find(2).mData)
print(a:Find(3).mData)
