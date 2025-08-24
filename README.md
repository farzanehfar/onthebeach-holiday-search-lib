# Holiday Search (Library) — .NET 8 / C#

Small, SOLID library that loads flights + hotels (JSON) and finds the **best value** holiday for a request.

## How to run tests

This project uses **xUnit**.  
From the project root:

```bash
cd tests/Otb.HolidaySearch.Tests
dotnet test -c Release
```

## How it works
- Two JSON files (flights.json and hotels.json) are loaded into memory.
#### Flights are filtered by:
-   Origin (supports "MAN", "Any Airport", "Any London Airport").
-   Destination.
-   Departure date.
  
#### Hotels are filtered by:

- Destination airport.

- Arrival date.

- Number of nights.

##### Each matching flight is combined with each matching hotel, and the total price is calculated.

##### The cheapest combination is returned.

## Why this design
- Clear models (Flight, Hotel, HolidaySearchRequest, HolidayResult).

- Single engine class (HolidaySearchEngine) with a small public API → very easy to unit test.

- Data access isolated (JsonDataStore) → JSON can be replaced with a DB or API later without changing the engine.

- No third-party packages → lightweight, only .NET.

#### SOLID principles:
- Separation of concerns: data loading, search logic, and models are separated.

## Project structure
```bash
onthebeach-holiday-search-lib/
├── data/                         # JSON datasets for flights and hotels
│   ├── flights.json
│   └── hotels.json
│
├── src/
│   └── Otb.HolidaySearch.Core/   # main library code
│       ├── Models/               # domain objects
│       │   ├── Flight.cs
│       │   ├── Hotel.cs
│       │   ├── HolidaySearchRequest.cs
│       │   └── HolidayResult.cs
│       │
│       ├── Data/                 # JSON loader
│       │   └── JsonDataStore.cs
│       │
│       ├── Search/               # main engine
│       │   └── HolidaySearchEngine.cs
│       │
│       └── Otb.HolidaySearch.Core.csproj
│
└── tests/
    └── Otb.HolidaySearch.Tests/  # xUnit tests
        ├── HolidaySearchExamplesTests.cs
        └── Otb.HolidaySearch.Tests.csproj

```

## Example results (from tests)
- Customer 1 → Manchester to Malaga, 2023-07-01, 7 nights
   ✅ Flight #2 + Hotel #9

- Customer 2 → Any London Airport to Mallorca, 2023-06-15, 10 nights
   ✅ Flight #6 + Hotel #5

- Customer 3 → Any Airport to Gran Canaria, 2022-11-10, 14 nights
   ✅ Flight #7 + Hotel #6

- No match → returns null
  
## TDD process

The repo was built following **Test-Driven Development (TDD)**, with each step reflected in the commit history:

1. **Scaffold repo**
   - Commit: `chore: scaffold repo with README and .gitignore`
   - Commit: `chore(core): add core library project file (.NET 8)`

2. **First failing test**
   - Commit: `test: add failing test for Customer 1 (Manchester → Malaga)`
   - Purpose: create red test before writing any code.

3. **Add minimal models**
   - Commit: `feat(core): add Flight model`
   - Commit: `feat(core): add Hotel model`
   - Commit: `feat(core): add HolidaySearchRequest`
   - Commit: `feat(core): add HolidayResult`
   - Purpose: enough structure to make tests compile.

4. **Make test pass**
   - Commit: `feat(core): implement holiday search engine (origin rules + cheapest pick)`
   - Purpose: implement smallest logic to satisfy Customer 1.

5. **Extend tests**
   - Commit: `test: add customer scenarios (Customer 2: Any London, Customer 3: Any Airport)`
   - Purpose: force new rules to emerge.

6. **Refactor + improve logic**
   - Commit: `feat(core): extend HolidaySearchEngine (Any London + Any Airport rules)`
   - Purpose: pass new failing tests.

7. **Add no-match case**
   - Commit: `test: add no-match test`
   - Purpose: handle null result gracefully.

8. **Add real data loader**
   - Commit: `feat(data): JsonDataStore + datasets (flights.json, hotels.json)`
   - Purpose: replace hardcoded lists with JSON.

9. **Polish**
   - Commit: `docs: update README with run instructions + design notes`
   - Purpose: documentation and cleanup.

👉 This sequence shows the **Red → Green → Refactor cycle**:  
- Red (failing tests first)  
- Green (minimal code to pass)  
- Refactor (clean, extract rules, add JSON store)


## Extend later

- Add interfaces and repositories if the data source changes (e.g., from JSON → database or API).
- Expose the search engine as a REST API (ASP.NET Core minimal API or controllers).
- Add multiple sorting modes (cheapest, best rated, fastest).
- Introduce caching (e.g., in-memory or Redis) to improve performance on repeated queries.
- Add CI/CD pipeline with automated test runs on GitHub Actions.
- Improve test coverage with edge cases (invalid dates, missing airports, overlapping data).
- Add Dockerfile to containerize the library and tests for consistent runtime across environments.
- **Restructure into microservices** if scaling is required:
  - Flight Service → owns flight data and logic.  
  - Hotel Service → owns hotel data and logic.  
  - Search Aggregator Service → calls both services, applies rules, returns results.  
  - Shared contracts via REST or gRPC for communication.

