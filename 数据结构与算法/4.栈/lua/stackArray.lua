local Stack = {
  mData = {},
  mLength = 0
}

function Stack:New()
  local o = {}
  setmetatable(o,self)
  self.__index = self
  o.mData = self.mData
  o.mLength = self.mLength

  return o
end

function Stack:Top()
  assert(self.mLength ~= 0,"the stack is empty")

  return self.mData[self.mLength]
end

function Stack:Pop()
  local currentData = self.mData[self.mLength]
  self.mData[self.mLength] = nil
  self.mLength = self.mLength - 1

  return currentData
end

function Stack:Push(value)
  self.mData[self.mLength+1] = value
  self.mLength = self.mLength + 1
end

local a = Stack:New()

a:Push(123)
a:Push(34)
a:Push(23)
a:Push(13)
a:Push(3)

print(a:Top())

print(a:Pop())

print(a:Top())