--lua中,函数可以存放在一个变量中，也可以当做另一个函数的入参和出参

function foo()
  print("function foo")
end

func = function ()
  print("func = function")
end

print(foo())
print(func())