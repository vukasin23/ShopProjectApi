# TODO

## Kritično


- [ ] **Dodeliti use case-ove novom korisniku pri registraciji**
  - `ShopProject.Implementation/Command/EfRegisterUserCommand.cs` ne dodeljuje `UseCases`.
  - Kad se uključi provera iznad, svaki novi korisnik bi dobijao 401 za sve.
  - Odlučiti šta sme običan korisnik (korpa, wishlist, porudžbine, adrese, profil), a šta samo admin.


## Treba srediti

- [ ] **Popraviti testove koji padaju (7 od 68)**
  - `CreateCartValidatorTests.Validate_WhenActorExistsAndHasNoCart_ShouldNotHaveValidationError`
  - `CreateProductSpecificationValidatorTests.Validate_WhenSpecificationAlreadyExistsForProduct_ShouldHaveValidationError`
  - `CreateProductSpecificationValidatorTests.Validate_WhenSpecificationValueIsTooShort_ShouldHaveValidationError`
  - `CreateProductSpecificationValidatorTests.Validate_WhenSpecificationNameIsTooShort_ShouldHaveValidationError`
  - `CreateInventoryValidatorTests.Validate_WhenLastUpdatedIsEmpty_ShouldHaveValidationError`
  - `CreateShippingMethodTests.Validate_WhenNameIsNotUnique_ShouldHaveValidationError`
  - `CreateProductValidatorTests.Validate_WhenNameIsNotUnique_ShouldHaveError`
  - Testovi jedinstvenosti rade nad pravom bazom (`new ShopProjectContext()`) i zavise od podataka u njoj.
  - Ostali izgledaju kao pravo neslaganje između testa i validatora.

- [ ] **Napisati testove za validatore koji ih nemaju**
  - `CreateCategoryValidator`, `CreateOrderValidator`, `CreateOrderLineValidator`, `RegisterUserValidator`, `OrderSearchValidator`
  - Svi `Update*` validatori

## Sitnice

- [ ] Obrisati `ShopProject.Implementation/DefaultActorProvider.cs` (prazna klasa, ne koristi se).
- [ ] Obrisati zastareo komentar `//Kreirati komande za --- Addresses, Coupons...` u `ContainerExtensions.cs`.
- [ ] Ograničiti `PerPage` u `PagedSearch` (sad može `PerPage=100000`); search validator postoji samo za `OrderSearch`.
- [ ] Obrisati preostale komentare `//validator` i `//execute registration logic` u `EfRegisterUserCommand.cs`.
- [ ] Pre deploya: proveriti `RequireHttpsMetadata = false` i redosled `UseHttpsRedirection` u `Program.cs`.
