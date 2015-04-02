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
  });
});


