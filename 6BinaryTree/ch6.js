////////////////// ES. A.1 ///////////////////////////
function insert(x,btree){
  if (btree(label) === x) return btree;
  if (btree(label) === null) return node(x, empty(), empty());
  if (btree(label) >= x) return node(btree(label), insert(x, btree(left)), btree(right));
  if (btree(label) <= x) return node(btree(label), btree(left), insert(x, btree(right)));
}

function insertFromList(list, binTree) {
  return list.reduce(function(acc, val) {
    return insert(val, acc);
  }, binTree);
}

function inorderToList(binTree) {
  if(binTree(label) === null) return [];
  return [].concat(inorderToList(binTree(left))).concat(binTree(label)).concat(inorderToList(binTree(right)));
}

function filterToList(pred, binTree) {
  console.log(printTree(binTree))
  if(binTree(label) === null) return [];
  if(pred(binTree(label))){
    return [].concat(filterToList(pred, binTree(left))).concat(binTree(label)).concat(filterToList(pred, binTree(right)));
  }
  return [].concat(filterToList(pred, binTree(left))).concat(filterToList(pred, binTree(right)));
}