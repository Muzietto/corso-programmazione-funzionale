var expect = chai.expect;

describe('chapter 6',function(){
  describe('before exercise A.1',function(){
    beforeEach(function(){
    });
    it('we verify that binary trees work fine',function(){
      expect(empty()(label)).to.be.null;
      expect(empty()(left)).to.be.null;
      expect(empty()(right)).to.be.null;
      expect(printTree(empty())).to.be.equal('empty()');

      var node3 = node(3,empty(),empty());
      expect(printTree(node3)).to.be.equal('node(3,empty(),empty())');
      expect(node3(label)).to.be.equal(3);
      expect(isEmptyLeaf(node3(left))).to.be.true;
      expect(node3(left)(label)).to.be.null;
      expect(isEmptyLeaf(node3(right))).to.be.true;
      expect(isEmptyLeaf(node3)).to.be.false;
      
      var nodes23 = node(2,empty(),node(3,empty(),empty()));
      expect(printTree(nodes23)).to.be.equal('node(2,empty(),node(3,empty(),empty()))');
      expect(nodes23(right)(label)).to.be.equal(3);
      expect(isEmptyLeaf(nodes23(right)(left))).to.be.true;
      expect(isEmptyLeaf(nodes23(right))).to.be.false;      
    });
  });
  describe('exercise A.1',function(){
    it('insert works are we wish',function(){
      var nodes4 = node(4,empty(),empty());
      var nodes34 = insert(3,nodes4);
      expect(printTree(nodes34)).to.be.equal('node(4,node(3,empty(),empty()),empty())');
      var nodesEmpty = insert(3, empty());
      expect(printTree(nodesEmpty)).to.be.equal('node(3,empty(),empty())');
      var nodes1234 = insert(2,insert(1,nodes34));
      expect(printTree(nodes1234)).to.be.equal('node(4,node(3,node(1,empty(),node(2,empty(),empty())),empty()),empty())');
      expect(nodes1234(left)(right)(label)).to.be.null;
      expect(nodes1234(left)(left)(right)(label)).to.be.equal(2);
    });
  });
  describe('exercise A.2',function(){
    beforeEach(function(){
      this.intList = [ 20, 10, 60, 15, 40, 100, 30, 50, 70, 35, 42, 58, 75, 32, 37 ];
      this.strList1 = [ "pesca", "banana", "uva", "albicocca", "nocciola", "ribes" ];
      this.strList2 = [ "limone", "ciliegia", "mela", "pera", "noce"  ];      
    });
    it('insertFromList produces intTree',function(){
      expect(printTree(insertFromList(this.intList, empty()))).to.be.equal('node(20,node(10,empty(),node(15,empty(),empty())),node(60,node(40,node(30,empty(),node(35,node(32,empty(),empty()),node(37,empty(),empty()))),node(50,node(42,empty(),empty()),node(58,empty(),empty()))),node(100,node(70,empty(),node(75,empty(),empty())),empty())))');      
    });
    it('insertFromList produces strTree1',function(){
      expect(printTree(insertFromList(this.strList1, empty()))).to.be.equal('node(pesca,node(banana,node(albicocca,empty(),empty()),node(nocciola,empty(),empty())),node(uva,node(ribes,empty(),empty()),empty()))');          
    });
    it('insertFromList produces strTree2',function(){
      expect(printTree(insertFromList(this.strList2, insertFromList(this.strList1, empty())))).to.be.equal('node(pesca,node(banana,node(albicocca,empty(),empty()),node(nocciola,node(limone,node(ciliegia,empty(),empty()),node(mela,empty(),empty())),node(pera,node(noce,empty(),empty()),empty()))),node(uva,node(ribes,empty(),empty()),empty()))');          
    });
  });
  describe('exercise A.3',function(){
    it.skip('intToFloatTree works are we wish',function(){
    });
  });
  describe('exercise B.1',function(){
    beforeEach(function(){
      this.intTree = insertFromList([ 20, 10, 60, 15, 40, 100, 30, 50, 70, 35, 42, 58, 75, 32, 37 ], empty());
      this.strTree1 = insertFromList([ "pesca", "banana", "uva", "albicocca", "nocciola", "ribes" ], empty());
      this.strTree2 = insertFromList([ "limone", "ciliegia", "mela", "pera", "noce"  ], this.strTree1);      
    });
    it('inorderToList works are we wish',function(){
      expect(inorderToList(this.intTree)).to.deep.equal([10, 15, 20, 30, 32, 35, 37, 40, 42, 50, 58, 60, 70, 75, 100]);
      expect(inorderToList(this.strTree1)).to.deep.equal(["albicocca", "banana", "nocciola", "pesca", "ribes", "uva"]);
      expect(inorderToList(this.strTree2)).to.deep.equal(["albicocca", "banana", "ciliegia", "limone", "mela", "nocciola", "noce", "pera", "pesca", "ribes", "uva"]);
    });
  });
  describe('exercise B.2',function(){
    beforeEach(function(){
      this.intTree = insertFromList([ 20, 10, 60, 15, 40, 100, 30, 50, 70, 35, 42, 58, 75, 32, 37 ], empty());
      this.strTree1 = insertFromList([ "pesca", "banana", "uva", "albicocca", "nocciola", "ribes" ], empty());
      this.strTree2 = insertFromList([ "limone", "ciliegia", "mela", "pera", "noce"  ], this.strTree1);  
      this.isEven = function(val){
        return (val % 2) === 0;        
      };
      this.isBig = function(val){
        return val >= 1000;        
      };
      this.isGood = function(val){
        return (val === "pesca" || val === "mela" || val === "fragola");
      };
      this.isNotGood = function(val){
        return !(val === "pesca" || val === "mela" || val === "fragola");
      };
    });
    it('filterToList works are we wish',function(){
      expect(filterToList(this.isEven, this.intTree)).to.deep.equal([10, 20, 30, 32, 40, 42, 50, 58, 60, 70, 100]);
      expect(filterToList(this.isBig, this.intTree)).to.deep.equal([]);
      expect(filterToList(this.isGood, this.strTree2)).to.deep.equal(["mela", "pesca"]);
      expect(filterToList(this.isNotGood, this.strTree2)).to.deep.equal(["albicocca", "banana", "ciliegia", "limone", "nocciola", "noce", "pera", "ribes", "uva"]);
    });
  });
  describe('exercise C.1',function(){
    beforeEach(function(){
      this.intTree = insertFromList([ 20, 10, 60, 15, 40, 100, 30, 50, 70, 35, 42, 58, 75, 32, 37 ], empty());
      this.strTree1 = insertFromList([ "pesca", "banana", "uva", "albicocca", "nocciola", "ribes" ], empty());
      this.strTree2 = insertFromList([ "limone", "ciliegia", "mela", "pera", "noce"  ], this.strTree1);      
    });
    it('search works are we wish',function(){
      expect(search(60, this.intTree)).to.be.true;
      expect(search('uva', this.strTree1)).to.be.true;
      expect(search('noce', this.strTree2)).to.be.true;
      expect(search(153, this.intTree)).to.be.false;
      expect(search('mela', this.strTree1)).to.be.false;
      expect(search('papaya', this.strTree2)).to.be.false;
    });
  });
  describe('exercise C.2',function(){
    beforeEach(function(){
      this.intTree = insertFromList([ 20, 10, 60, 15, 40, 100, 30, 50, 70, 35, 42, 58, 75, 32, 37 ], empty());
    });
    it('searchPath works are we wish',function(){
      expect(searchPath(10, this.intTree)).to.deep.equal([20, 10]);
      expect(searchPath(20, this.intTree)).to.deep.equal([20]);
      expect(searchPath(40, this.intTree)).to.deep.equal([20, 60, 40]);
      expect(searchPath(32, this.intTree)).to.deep.equal([20, 60, 40, 30, 35, 32]);
      expect(searchPath(11, this.intTree)).to.deep.equal([]);
    });
  });
  describe('exercise D.1',function(){
    beforeEach(function(){
      this.intTree = insertFromList([ 20, 10, 60, 15, 40, 100, 30, 50, 70, 35, 42, 58, 75, 32, 37 ], empty());
      this.strTree1 = insertFromList([ "pesca", "banana", "uva", "albicocca", "nocciola", "ribes" ], empty());
      this.strTree2 = insertFromList([ "limone", "ciliegia", "mela", "pera", "noce"  ], this.strTree1);  
    });
    it('count works are we wish',function(){
      expect(count(this.intTree)).to.deep.equal({node: 15, leaf: 6});
      expect(count(this.strTree1)).to.deep.equal({node: 6, leaf: 3});
      expect(count(this.strTree2)).to.deep.equal({node: 11, leaf: 5});
    });
  });
  describe('exercise D.2',function(){
    beforeEach(function(){
      this.intTree = insertFromList([ 20, 10, 60, 15, 40, 100, 30, 50, 70, 35, 42, 58, 75, 32, 37 ], empty());
      this.strTree1 = insertFromList([ "pesca", "banana", "uva", "albicocca", "nocciola", "ribes" ], empty());
      this.strTree2 = insertFromList([ "limone", "ciliegia", "mela", "pera", "noce"  ], this.strTree1);  
    });
    it('min works are we wish',function(){
      expect(value(min(this.intTree))).to.deep.equal(10);  
      expect(value(min(this.strTree2))).to.deep.equal('albicocca');  
      expect(value(min(empty()))).to.be.null;  
    });
  });
  describe('exercise D.3',function(){
    beforeEach(function(){
      this.intTree = insertFromList([ 20, 10, 60, 15, 40, 100, 30, 50, 70, 35, 42, 58, 75, 32, 37 ], empty());
      this.strTree1 = insertFromList([ "pesca", "banana", "uva", "albicocca", "nocciola", "ribes" ], empty());
      this.strTree2 = insertFromList([ "limone", "ciliegia", "mela", "pera", "noce"  ], this.strTree1);  
    });
    it('subtree works are we wish',function(){
      expect(value(min(subtree(10, this.intTree)))).to.deep.equal(10);
      expect(value(min(subtree(15, this.intTree)))).to.deep.equal(15);
      expect(value(min(subtree(60, this.intTree)))).to.deep.equal(30);
      expect(value(min(subtree(40, this.intTree)))).to.deep.equal(30);
      expect(value(min(subtree(100, this.intTree)))).to.deep.equal(70);
      expect(value(min(subtree(1000, this.intTree)))).to.be.null;
      expect(value(min(subtree ("limone",  this.strTree2)))).to.deep.equal('ciliegia');
      expect(value(min(subtree ("ribes",  this.strTree2)))).to.deep.equal('ribes');
    });
  });
  describe('exercise D.4',function(){
    beforeEach(function(){
      this.intTree = insertFromList([ 20, 10, 60, 15, 40, 100, 30, 50, 70, 35, 42, 58, 75, 32, 37 ], empty());
    });
    it('depthToList works are we wish',function(){
    expect(depthToList(0, this.intTree)).to.deep.equal([20]);
    expect(depthToList(1, this.intTree)).to.deep.equal([10, 60]);
    expect(depthToList(2, this.intTree)).to.deep.equal([15, 40, 100]);
    expect(depthToList(3, this.intTree)).to.deep.equal([30, 50, 70]);
    expect(depthToList(4, this.intTree)).to.deep.equal([35, 42, 58, 75]);
    expect(depthToList(5, this.intTree)).to.deep.equal([32, 37]);
    expect(depthToList(6, this.intTree)).to.deep.equal([]);
    expect(depthToList(100, this.intTree)).to.deep.equal([]);
    });
  });
});



