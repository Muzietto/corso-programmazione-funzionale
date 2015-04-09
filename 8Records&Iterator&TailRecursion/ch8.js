'use strict';
function valv(name, grade) {

  return {stud: name, voto: grade};
}

function valuta(record) {

  if (record.voto < 18) {
    return {id: record.stud, giudizio: 'insufficiente'};
  }
  if (record.voto <= 22) {
    return {id: record.stud, giudizio: 'sufficiente'};
  }
  if (record.voto <= 27) {
    return {id: record.stud, giudizio: 'buono'};
  }
  return {id: record.stud, giudizio: 'ottimo'};
}

function valutaListr(listOFRecords) {

  return listOFRecords.map(valuta);
}

function creaValListR(studenti, voti) {

  if(studenti[0] === undefined) {
    return [];
  }
  return [{stud: studenti[0], voto: voti[0]}].concat(creaValListR(studenti.slice(1, studenti.length), voti.slice(1, studenti.length)));
}
