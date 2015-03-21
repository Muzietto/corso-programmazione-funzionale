
function sumWithFoldl(array){
  return foldl(array,function(acc,curr){ return acc + curr; },0);
}