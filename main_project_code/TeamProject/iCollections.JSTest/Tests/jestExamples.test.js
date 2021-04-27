import {sum} from '../../iCollections/wwwroot/js/jest_testing_functions'
//const sum = require('./sum');

test('3 to equal 3', () => {
    //Arrange
    
    //Act

    //Assert
    expect(3).toBe(3);
});


test('sum of 3 and 3 to equal 6', () => {
    //Arrange
    
    //Act

    //Assert
    expect(sum(3,3)).toBe(6);
});


test('two plus two is four', () => {
    expect(2 + 2).toBe(4);
});


test('object assignment', () => {
const data = {one: 1};
data['two'] = 2;
expect(data).toEqual({one: 1, two: 2});
});

test('adding positive numbers is not zero', () => {
    for (let a = 1; a < 10; a++) {
      for (let b = 1; b < 10; b++) {
        expect(a + b).not.toBe(0);
      }
    }
  });

  test('null', () => {
    const n = null;
    expect(n).toBeNull();
    expect(n).toBeDefined();
    expect(n).not.toBeUndefined();
    expect(n).not.toBeTruthy();
    expect(n).toBeFalsy();
  });
  
  test('zero', () => {
    const z = 0;
    expect(z).not.toBeNull();
    expect(z).toBeDefined();
    expect(z).not.toBeUndefined();
    expect(z).not.toBeTruthy();
    expect(z).toBeFalsy();
  });

////////NUMBERS EXAMPLES 
  test('two plus two', () => {
    const value = 2 + 2;
    expect(value).toBeGreaterThan(3);
    expect(value).toBeGreaterThanOrEqual(3.5);
    expect(value).toBeLessThan(5);
    expect(value).toBeLessThanOrEqual(4.5);
  
    // toBe and toEqual are equivalent for numbers
    expect(value).toBe(4);
    expect(value).toEqual(4);
  });

  test('adding floating point numbers', () => {
    const value = 0.1 + 0.2;
    //expect(value).toBe(0.3);           This won't work because of rounding error
    expect(value).toBeCloseTo(0.3); // This works.
  });

////////STRINGS EXAMPLES 
test('there is no I in team', () => {
    expect('team').not.toMatch(/I/);
  });
  
  test('but there is a "stop" in Christoph', () => {
    expect('Christoph').toMatch(/stop/);
  });

////////Arrays and iterables EXAMPLES 
const shoppingList = [
    'diapers',
    'kleenex',
    'trash bags',
    'paper towels',
    'beer',
  ];
  
  test('the shopping list has beer on it', () => {
    expect(shoppingList).toContain('beer');
    expect(new Set(shoppingList)).toContain('beer');
  });

////////Exceptions EXAMPLES

function compileAndroidCode() {
    throw new Error('you are using the wrong JDK');
  }
  
  test('compiling android goes as expected', () => {
    expect(() => compileAndroidCode()).toThrow();
    expect(() => compileAndroidCode()).toThrow(Error);
  
    // You can also use the exact error message or a regexp
    expect(() => compileAndroidCode()).toThrow('you are using the wrong JDK');
    expect(() => compileAndroidCode()).toThrow(/JDK/);
  });


  //Sample tests for objects and arrays 
  test('id should match', () => {
    const obj = {
      id: '111',
      productName: 'Jest Handbook',
      url: 'https://jesthandbook.com'
    };
    expect(obj.id).toEqual('111');
  });
  
  test('id and productName should match', () => {
    const obj = {
      id: '111',
      productName: 'Jest Handbook',
      url: 'https://jesthandbook.com'
    };
    expect(obj.id).toEqual('111');
    expect(obj.productName).toEqual('Jest Handbook');
  });

  test('id and productName should match', () => {
    const obj = {
      id: '111',
      productName: 'Jest Handbook',
      url: 'https://jesthandbook.com'
    };
    expect(obj).toEqual(
      expect.objectContaining({
        id: '111',
        productName: 'Jest Handbook'
      })
    );
  });

  const oddArray = [1, 3, 5, 7, 9, 11, 13];
test('should start correctly', () => {
  expect(oddArray).toEqual(expect.arrayContaining([1, 3, 5, 7, 9]));
});


const users = [{id: 1, name: 'Hugo'}, {id: 2, name: 'Francesco'}];

test('we should have ids 1 and 2', () => {
  expect(users).toEqual(
    expect.arrayContaining([
      expect.objectContaining({id: 1}),
      expect.objectContaining({id: 2})
    ])
  );
});


const user = {
  id: 1,
  name: 'Hugo',
  friends: [3, 5, 22]
};
test('user 3 should be a friend of user', () => {
  expect(user).toEqual(
    expect.objectContaining({
      friends: expect.arrayContaining([3])
    })
  );
});
