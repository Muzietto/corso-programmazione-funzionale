
////////////////// ES. A.1 ///////////////////////////
function insert(x, btree) {

  if (btree(label) === x) {
    return btree;
  }
  if (btree(label) === null) {
    return node(x, empty(), empty());
  }
  if (btree(label) >= x) {
    return node(btree(label), insert(x, btree(left)), btree(right));
  }
  if (btree(label) <= x) {
    return node(btree(label), btree(left), insert(x, btree(right)));
  }
}

////////////////// ES. A.2 ///////////////////////////
function insertFromList(list, binTree) {

  return list.reduce(function (acc, val) {
    return insert(val, acc);
  }, binTree);
}

////////////////// ES. B.1 ///////////////////////////
function inorderToList(binTree) {

  if (binTree(label) === null) {
    return [];
  }
  return [].concat(inorderToList(binTree(left))).concat(binTree(label)).concat(inorderToList(binTree(right)));
}

////////////////// ES. B.2 ///////////////////////////
function filterToList(pred, binTree) {

  if (binTree(label) === null) {
    return [];
  }
  if (pred(binTree(label))) {
    return [].concat(filterToList(pred, binTree(left))).concat(binTree(label)).concat(filterToList(pred, binTree(right)));
  }
  return [].concat(filterToList(pred, binTree(left))).concat(filterToList(pred, binTree(right)));
}

////////////////// ES. C.1 ///////////////////////////
function search(ele, binTree) {

  if (binTree(label) === null) {
    return false;
  }
  if (binTree(label) === ele) {
    return true;
  }
  if (binTree(label) >= ele) {
    return search(ele, binTree(left));
  }
  if (binTree(label) <= ele) {
    return search(ele, binTree(right));
  }
}

////////////////// ES. C.2 ///////////////////////////
function searchPath(ele, binTree) {

  var path = [];
  return helper(ele, binTree);
  function helper(elem, tree) {
    if (tree(label) === null) {
      return [];
    }
    if (tree(label) === elem) {
      return path.concat(elem);
    }
    if (tree(label) >= elem) {
      path = path.concat(tree(label));
      return helper(elem, tree(left));
    }
    if (tree(label) <= elem) {
      path = path.concat(tree(label));
      return helper(elem, tree(right));
    }
  }
}

////////////////// ES. D.1 ///////////////////////////
function count(binTree) {

  var result = {node: 0, leaf: 0};
  return helper(binTree);
  function helper(tree) {
    if (tree(label) === null) {
      return result;
    }
    if (isEmptyLeaf(tree(left)) && isEmptyLeaf(tree(right))) {
      result.node = result.node + 1;
      result.leaf = result.leaf + 1;
      return result;
    }
    if (isEmptyLeaf(tree(left))) {
      result.node = result.node + 1;
      helper(tree(right));
      return result;
    }
    if (isEmptyLeaf(tree(right))) {
      result.node = result.node + 1;
      helper(tree(left));
      return result;
    }
    result.node = result.node + 1;
    helper(tree(left));
    helper(tree(right));
    return result;
  }
}

////////////////// ES. D.2 ///////////////////////////
function min(binTree) {

  if (binTree(label) === null) {
    return none();
  }
  if (!isEmptyLeaf(binTree(left))) {
    return min(binTree(left));
  }
  return some(binTree(label));
}

////////////////// ES. D.3 ///////////////////////////
function subtree(ele, binTree) {

  if (binTree(label) === null) {
    return empty();
  }
  if (binTree(label) === ele) {
    return node(ele, binTree(left), binTree(right));
  }
  if (binTree(label) >= ele) {
    return subtree(ele, binTree(left));
  }
  if (binTree(label) <= ele) {
    return subtree(ele, binTree(right));
  }
}

////////////////// ES. D.4 ///////////////////////////
function depthToList(depth, binTree) {

  var node = [];
  return helper(depth, binTree);
  function helper(dep, tree) {
    if (tree(label) === null) {
      return node;
    }
    if (dep === 0) {
      node = node.concat(tree(label));
      return node;
    }
    helper(dep - 1, tree(left));
    helper(dep - 1, tree(right));
    return node;
  }
}
