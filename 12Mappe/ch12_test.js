var expect = chai.expect;

describe('in chapter 12',function(){
  describe('MAP IMPLEMENTATION',function(){
    it('empyMap and map works as expected',function(){
      expect(map.empty()).to.be.eql({});
      expect(map.ofList([["Bianchi",25], ["Rossi",28], ["Verdi",25]])).to.be.eql({Bianchi: 25, Rossi: 28, Verdi: 25});
    });
    it('dom works as expected',function(){
      expect(map.dom(map.ofList([["Bianchi",25], ["Rossi",28], ["Verdi",25]]))).to.be.eql(['Bianchi', 'Rossi', 'Verdi']);
    });
    it('toList works as expected',function(){
      expect(map.toList(map.ofList([["Bianchi",25], ["Rossi",28], ["Verdi",25]]))).to.be.eql([["Bianchi",25], ["Rossi",28], ["Verdi",25]]);
    });
    it('add works as expected',function(){
      expect(map.add('Bianchi', 25, map.add('Rossi' , 28, map.add('Verdi', 25, map.empty())))).to.be.eql({Bianchi: 25, Rossi: 28, Verdi: 25});
    });
    it('containsKey works as expected',function(){
      expect(map.containsKey('Bianchi', {Bianchi: 25, Rossi: 28, Verdi: 25})).to.be.true;
      expect(map.containsKey('Gialli', {Bianchi: 25, Rossi: 28, Verdi: 25})).to.be.false;
    });
    it('find works as expected',function(){
      expect(map.find('Bianchi', {Bianchi: 25, Rossi: 28, Verdi: 25})).to.be.equal(25);
      expect(map.find.bind(null, 'Gialli', {Bianchi: 25, Rossi: 28, Verdi: 25})).to.throw(ReferenceError);
    });
    it('tryFind works as expected',function(){
      expect(value(map.tryFind('Bianchi', {Bianchi: 25, Rossi: 28, Verdi: 25}))).to.be.equal(value(some(25)));
      expect(isNone(map.tryFind(null, 'Gialli', {Bianchi: 25, Rossi: 28, Verdi: 25}))).to.be.true;
    });
    it('map works as expected',function(){
      expect(map.map(function(key, val) {return 0;}, {Bianchi: 25, Rossi: 28, Verdi: 25})).to.be.eql({Bianchi: 0, Rossi: 0, Verdi: 0});
    });
    it('filter works as expected',function(){
      expect(map.filter(function(key) {return key % 2 === 0;}, {2: 25, 5: 28, 6: 25})).to.be.eql({2: 25, 6: 25});
    });
    it('exists works as expected',function(){
      expect(map.exists(function(key) {return key % 2 === 0;}, {2: 25, 5: 28, 6: 25})).to.be.true;
    });
    it('forall works as expected',function(){
      expect(map.forall(function(key) {return key % 2 === 0;}, {2: 25, 8: 28, 6: 25})).to.be.true;
      expect(map.forall(function(key) {return key % 2 === 0;}, {2: 25, 5: 28, 6: 25})).to.be.false;
    });
    it('fold works as expected',function(){
      expect(map.fold(function(acc, key, value) {return acc + value}, 0, {2: 25, 8: 28, 6: 25})).to.be.equal(78);
    });
  });
});