var expect = chai.expect;

describe('chapter 8', function () {
  describe('exercise on modelling records with object', function () {
    beforeEach(function(){
    });
    it('valv model correctly a student grade object',function(){
      expect(valv('Bianchi', 25)).to.be.eql({stud: 'Bianchi', voto: 25});
      expect(valv('Rossi', 18)).to.be.eql({stud: 'Rossi', voto: 18});
      expect(valv('Gialli', 30)).to.be.eql({stud: 'Gialli', voto: 30});
    });
  });
  describe('valuta works as expected for valv object', function () {
    beforeEach(function(){
      this.rossiRec = valv('Rossi', 18);
      this.bianchiRec = valv('Bianchi', 25);
      this.gialliRec = valv('Gialli', 30);
    });
    it('valv model correctly a student grade object',function(){
      expect(valuta(this.rossiRec)).to.be.eql({id: 'Rossi', giudizio: 'sufficiente'});
      expect(valuta(this.bianchiRec)).to.be.eql({id: 'Bianchi', giudizio: 'buono'});
      expect(valuta(this.gialliRec)).to.be.eql({id: 'Gialli', giudizio: 'ottimo'});
    });
  });
  describe('valutaListr works as expected for a list of valv object', function () {
    beforeEach(function(){
      this.studss = [valv('Rossi', 20), valv('Verdi', 24),valv('Neri', 29), valv('Bianchi', 16)];
    });
    it('valv model correctly a student grade object',function(){
      expect(valutaListr(this.studss)).to.be.eql([{id: 'Rossi', giudizio: 'sufficiente'}, {id: 'Verdi', giudizio: 'buono'}, {id: 'Neri', giudizio: 'ottimo'}, {id: 'Bianchi', giudizio: 'insufficiente'}]);
    });
  });
  describe('creaValListR works as expected for a list of valv object', function () {
    beforeEach(function(){
      this.studss = [valv('Rossi', 20), valv('Verdi', 24),valv('Neri', 29), valv('Bianchi', 16)];
      this.studenti = ['Rossi', 'Verdi', 'Neri', 'Bianchi'];
      this.voti = [20, 24, 29, 16];
    });
    it('valv model correctly a student grade object',function(){
      expect(creaValListR(this.studenti, this.voti)).to.be.eql(this.studss);
    });
  });
});