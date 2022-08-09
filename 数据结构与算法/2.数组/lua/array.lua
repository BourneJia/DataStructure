ArrayList = {
  mCap = 0,
  mData = {},
  mLength = 0,
}

function ArrayList:New(capacity)
  self.mCap = capacity
  self.mData = {}
  self.mLength = 0
end

function ArrayList:Insert(index, data)
  assert(type(index) == "int", "index is not a int")
  assert(self.mCap == self.mData, "数组没有多余空间进行添加")
  assert(index < 0 and index >= self.mCap, "数组下标超出数组范围")

  for i = self.mLength-1, self.mLength-index, -1 do
    self.mData[i] = self.mData[i-1]
  end

  self.mData[index] = data
  self.mLength = self.mLength + 1

  return true
end

function ArrayList:Find(index)
  assert(type(index) == "int", "index is not a int")
  assert(index < 0 and index >= self.mCap, "数组下标超出数组范围")

  result["result"] = true
  result["data"] = self.mData[index]

  return result
end

function ArrayList:Delete(index)
  assert(type(index) == "int", "index is not a int")
  assert(index < 0 and index >= self.mLength, "数组下标超出数组范围")

  data = self.mData[index]
  while(self.mData[index] ~= nil)
  do
    self.mData[index] = self[index+1]
    index = index+1
  end

  result["result"] = true
  result["data"] = data

  return result
end