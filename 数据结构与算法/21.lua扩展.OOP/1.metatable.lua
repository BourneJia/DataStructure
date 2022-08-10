--元表，元表的表现行为类似于操作符重载，比如我们可以重载 __add，来计算两个 Lua 数组的并集；或者重载 __tostring，来定义转换为字符串的函数。
--setmetatable(table, metatable), 用于为一个 table 设置元表；
--getmetatable(table)，用于获取 table 的元表。

local version = {
  major = 1,
  minor = 1,
  patch = 1
}

version = setmetatable(version, {
  __tostring = function(t)
    return string.format( "%d.%d.%d", t.major, t.minor, t.patch)
  end
})

print(tostring(version))

--__index:在 table 中查找一个元素时，首先会直接从 table 中查询，如果没有找到，就继续到元表的 __index 中查询

local versionE = {
  major = 1,
  minor = 1
}

versionE = setmetatable(versionE, {
  __index = function(t,key)
    if key == "patch" then
      return 2
    end
  end,
  __tostring = function(t)
    return string.format("%d.%d.%d", t.major, t.minor, t.patch)
  end
})

print(tostring(versionE))--输出1.1.2
print(versionE.patch)--输出2

--__index不仅可以是一个函数，也可以是一个 table。
local versionX = {
  major = 1,
  minor = 1
}

versionX = setmetatable(versionX, {
  __index = {patch = 2},
  __tostring = function(t)
    return string.format("%d.%d.%d", t.major, t.minor, t.patch)
  end
})

print(tostring(versionX))--输出1.1.2

--__call。它类似于仿函数，可以让 table 被调用

local versionY = {
  major = 1,
  minor = 1,
  patch = 1
}

versionY = setmetatable(versionY,{
  __call = function(t)
    print(string.format("%d.%d.%d", t.major, t.minor, t.patch))
  end
})

versionY()

local mt = { __index = versionY }

local a = setmetatable({ b = 2 }, mt)

print(a.b)
print(a.major)
print(mt.major)

-- local mt = {
--   __index = versionY,
--   __call = function(t)
--     print(string.format("%d.%d.%d", t.major, t.minor, t.patch))
--   end
-- }

-- mt()