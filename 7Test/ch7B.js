'use strict';
function surname(student) {

	return function (w) {
		return w(student);
	};
}

function category(abbr) {
  return function(w){
    return w({
      'd' : 'Daycare',
      'n' : 'Nursery',
      'r' : 'Recreation'
    }[abbr]);
  }
}

function childDes(name, cat) {

	return function (w) {
		return w(name, cat);
	};
}

function stampChildDes(childDes) {

	return childDes(function (name, cat) {
		return value(name) + ': ' + value(cat);
	});
}

function number(category, childColl) {

	if (childColl[0] === undefined) {
		return 0;
	}
	if (takeCat(childColl[0]) === value(category)) {
		return 1 + number(category, childColl.slice(1, childColl.length));
	}
	return number(category, childColl.slice(1, childColl.length));
}

function takeCat(childDes) {

	return childDes(function (name, dest) {
		return value(dest);
	});
}

function pay(name, childColl) {

	var sibiling = false;

	function helper(helperChildColl) {
		var money;
		if (helperChildColl[0] === undefined) {
			return 0;
		}
		if (takeSurname(helperChildColl[0]) === value(name)) {
			money = sibiling ? costPerCategory(helperChildColl[0]) / 2 : costPerCategory(helperChildColl[0]);
			sibiling = true;
			return money + helper(helperChildColl.slice(1, helperChildColl.length));
		}
		return helper(helperChildColl.slice(1, helperChildColl.length));
	}

	return helper(childColl);
}

function costPerCategory(childDes) {

	var cat = takeCat(childDes);
	if (cat === 'Daycare') {
		return 225;
	}
	if (cat === 'Nursery') {
		return 116;
	}
	if (cat === 'Recreation') {
		return 110;
	}
}

function takeSurname(childDes) {

	return childDes(function (name, dest) {
		return value(name);
	});
}
