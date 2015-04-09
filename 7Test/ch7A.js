'use strict';
function grade(student, mark) {

  return function (w) {
    return w(student, mark);
  };
}

function printGrade(grade) {

  return grade(function (student, mark) {
    return student + ': ' + mark;
  });
}

function printListOfGrade(listaOfGrade) {

  return listaOfGrade.map(function (val) {
    return printGrade(val);
  }).join(', ');
}

function valuta(student, mark) {

  if (mark < 18) {
    return grade(student, 'insufficiente');
  }
  if (mark <= 22) {
    return grade(student, 'sufficiente');
  }
  if (mark <= 27) {
    return grade(student, 'buono');
  }
  return grade(student, 'ottimo');
}

function fromMarkToGrade(mark) {

  return mark(valuta);
}

function valutaList(lista) {

  var result = [];
  return helper(lista);
  function helper(lista) {
    if (lista.length === 0) {
      return result;
    }
    result = result.concat(fromMarkToGrade(lista[0]));
    return helper(lista.slice(1, lista.length));
  }
}

function creaValList(names, grades) {

  var minLength = Math.min(names.length, grades.length), result = [], i;
  for (i = 0; i < minLength; i = i + 1) {
    result = result.concat(grade(names[i], grades[i]));
  }
  return result;
}

function media(lista) {

  var sommaVoti = 0, numeroVoti = 0;
  function sommaAndConta(lista) {
    if (lista.length > 0) {
      sommaVoti = sommaVoti + lista[0](function (student, mark) {
        return mark;
      });
      numeroVoti = numeroVoti + 1;
      sommaAndConta(lista.slice(1, lista.length));
    }
  }
  sommaAndConta(lista);
  return sommaVoti / numeroVoti;
}

function separa(lista) {

  var result;
  result = lista.reduce(function (acc, val) {
    if (val(function (std, grade) {return grade; }) >= 18) {
      acc.promossi = acc.promossi.concat(val);
      return acc;
    }
    acc.bocciati = acc.bocciati.concat(val);
    return acc;
  }, {promossi: [], bocciati: []});
  result.promossi = printListOfGrade(result.promossi);
  result.bocciati = printListOfGrade(result.bocciati);
  return result;
}
