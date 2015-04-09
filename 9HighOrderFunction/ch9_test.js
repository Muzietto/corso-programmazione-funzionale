var expect = chai.expect;

describe('chapter 9', function () {
  describe('EXERCISE 2', function () {
    beforeEach(function(){
      this.quadrato = function (x) {
        return x * x;
      };
      this.tipo = function (x) {
        if(x % 2 === 0) {
          return {val: x, tipo: 'pari'};
        }
        return {val: x, tipo: 'dispari'};  
      };
      this.intList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    });
    it('reinventing the wheel, map implementation and some example on his usage',function(){
      expect(map(this.quadrato, this.intList)).to.be.eql([1, 4, 9, 16, 25, 36, 49, 64, 81, 100]);
      expect(map(this.tipo, this.intList)).to.be.eql([{val: 1, tipo: "dispari"}, {val: 2, tipo: "pari"}, {val: 3, tipo: "dispari"}, {val: 4, tipo: "pari"}, {val: 5, tipo: "dispari"}, {val: 6, tipo: "pari"}, {val: 7, tipo: "dispari"}, {val: 8, tipo: "pari"}, {val: 9, tipo: "dispari"}, {val: 10, tipo: "pari"}]);
    });
  });
  describe('EXERCISE 2 R', function () {
    beforeEach(function(){
      this.quadrato = function (x) {
        return x * x;
      };
      this.tipo = function (x) {
        if(x % 2 === 0) {
          return {val: x, tipo: 'pari'};
        }
        return {val: x, tipo: 'dispari'};  
      };
      this.intList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    });
    it('reinventing the wheel, map recursive implementation and some example on his usage',function(){
      expect(mapR(this.quadrato, this.intList)).to.be.eql([1, 4, 9, 16, 25, 36, 49, 64, 81, 100]);
      expect(mapR(this.tipo, this.intList)).to.be.eql([{val: 1, tipo: "dispari"}, {val: 2, tipo: "pari"}, {val: 3, tipo: "dispari"}, {val: 4, tipo: "pari"}, {val: 5, tipo: "dispari"}, {val: 6, tipo: "pari"}, {val: 7, tipo: "dispari"}, {val: 8, tipo: "pari"}, {val: 9, tipo: "dispari"}, {val: 10, tipo: "pari"}]);
    });
  });
  describe('EXERCISE 3', function () {
    beforeEach(function(){
      this.pari = function (x) {
        return x % 2 === 0;
      };
      this.intList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    });
    it('reinventing the wheel, filter implementation and some example on his usage',function(){
      expect(filter(this.pari, this.intList)).to.be.eql([2, 4, 6, 8, 10]);
    });
  });
  describe('EXERCISE 3 R', function () {
    beforeEach(function(){
      this.pari = function (x) {
        return x % 2 === 0;
      };
      this.intList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    });
    it('reinventing the wheel, filter recursive implementation and some example on his usage',function(){
      expect(filterR(this.pari, this.intList)).to.be.eql([2, 4, 6, 8, 10]);
    });
  });
  describe('EXERCISE 4', function () {
    beforeEach(function(){
      this.multiple3 = function (x) {
        return x % 3 === 0;
      };
      this.intList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20];
    });
    it('filter1 works as expected',function(){
      expect(filter1(this.multiple3, this.intList)).to.be.eql({correct: [3, 6, 9, 12, 15, 18], incorrect: [1, 2, 4, 5, 7, 8, 10, 11, 13, 14, 16, 17, 19, 20]});
    });
  });
  describe('EXERCISE 5', function () {
    beforeEach(function(){
    });
    it('divisori works as expected',function(){
      expect(divisori(100)).to.be.eql([1, 2, 4, 5, 10, 20, 25, 50, 100]);
    });
    it('isPrime works as expected',function(){
      expect(isPrime(100)).to.be.false;
      expect(isPrime(3)).to.be.true;
    });
  });
});