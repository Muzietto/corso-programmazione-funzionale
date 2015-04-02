var binTree = (function() {

  // INTERNAL USE
  function node(label,left,right){
    return function(nodeFun,leafFun){
      return nodeFun(label,left,right);
    };
  }

  function isEmptyLeaf(tree){
    return tree(
      function(){ return false; },
      function(){ return true; }
    );
  }

  function label(lbl,lf,rt){
    return lbl;
  }
  function left(lbl,lf,rt){
    return lf;
  }
  function right(lbl,lf,rt){
    return rt;
  }
  
  function subtree(ele, binTree) {
    'use strict';
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

  //PUBLIC
  function empty(){
    return function(nodeFun,leafFun){
      return leafFun ? leafFun() : null;
    };
  }
  
  function isEmpty(tree){
    return tree(label) === null;
  }
  
  function printTree(tree){
    return tree(
      function(label,left,right){
        return 'node(' + label + ',' + printTree(left) + ',' + printTree(right) + ')';
      },
      function(){ return 'empty()'; }
    );
  }

  function insert(x, btree) {
    'use strict';
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

  function insertFromList(list, binTree) {
    'use strict';
    return list.reduce(function (acc, val) {
      return insert(val, acc);
    }, binTree);
  }

  function inorderToList(binTree) {
    'use strict';
    if (binTree(label) === null) {
      return [];
    }
    return [].concat(inorderToList(binTree(left))).concat(binTree(label)).concat(inorderToList(binTree(right)));
  }

  function filterToList(pred, binTree) {
    'use strict';
    if (binTree(label) === null) {
      return [];
    }
    if (pred(binTree(label))) {
      return [].concat(filterToList(pred, binTree(left))).concat(binTree(label)).concat(filterToList(pred, binTree(right)));
    }
    return [].concat(filterToList(pred, binTree(left))).concat(filterToList(pred, binTree(right)));
  }

  function search(ele, binTree) {
    'use strict';
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

  function searchPath(ele, binTree) {
    'use strict';
    var path = [];
    return helper(ele, binTree);
    function helper(elem, tree) {
      if (tree(label) === null) {
        return [];
      }
      if (tree(label) === elem) {
        path.push(elem);
        return path;
      }
      if (tree(label) >= elem) {
        path.push(tree(label));
        return helper(elem, tree(left));
      }
      if (tree(label) <= elem) {
        path.push(tree(label));
        return helper(elem, tree(right));
      }
    }
  }

  function count(binTree) {
    'use strict';
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

  function min(binTree) {
    'use strict';
    if (binTree(label) === null) {
      return null;
    }
    if (!isEmptyLeaf(binTree(left))) {
      return min(binTree(left));
    }
    return binTree(label);
  }
  
  function max(binTree) {
    'use strict';
    if (binTree(label) === null) {
      return null;
    }
    return binTree(label);
  }

  function depthToList(depth, binTree) {
    'use strict';
    var node = [];
    return helper(depth, binTree);
    function helper(dep, tree) {
      if (tree(label) === null) {
        return node;
      }
      if (dep === 0) {
        node.push(tree(label));
        return node;
      }
      helper(dep - 1, tree(left));
      helper(dep - 1, tree(right));
      return node;
    }
  }
 
  function remove(elem, binTree) {
    var helperMin;
    if (binTree(label) === null) {
      return binTree;
    }
    if (binTree(label) === elem && binTree(left) === null && binTree(right) === null) {
      return empty();
    }
    if (binTree(label) === elem && binTree(left) === null) {
      return subtree(binTree(right));
    }
    if (binTree(label) === elem && binTree(right) === null) {
      return subtree(binTree(left)); 
    }
    if (binTree(label) === elem) {
      helperMin = min(binTree(right));
      if (helperMin === null) {
        return empty();      
      }
      return node(helperMin, binTree(left), remove(helperMin, binTree(right)));
    }
    return node(binTree(label), remove(elem, binTree(left)), remove(elem, binTree(right)));
  }
 
  function filter(pred, binTree) {
    if (binTree(label) === null) {
      return binTree;
    }  
    if (pred(binTree(label))) {
      return node(binTree(label), filter(pred, binTree(left)), filter(pred, binTree(right)));
    }  
    return filter(pred, remove(binTree(label), binTree));
  }
 
  return {
    empty: empty,
    isEmpty: isEmpty,
    toString: printTree,
    insert: insert,
    insertFromList: insertFromList,
    toList: inorderToList,
    filterToList: filterToList,
    filter: filter, 
    search: search,
    searchPath: searchPath,
    remove: remove,
    count: count,
    min: min,
    max: max,
    depthToList: depthToList    
  };
} ());