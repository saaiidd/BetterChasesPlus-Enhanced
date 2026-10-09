# Asset Marketplaces and Content Libraries for a Photorealistic UE5 Espionage/Urban Game (Specialist 1 of 12)

Research date: 2026-10-09. All "accessed" dates below are 2026-10-09 unless stated. Method notes: direct fetching of fab.com was blocked from the sandbox (proxy 403 / DNS failure), so Fab pages were read through the Exa crawler cache. Fab search-result "card" prices come from those cached crawls (crawl timestamp is not shown on the cards; individual listing crawls show dates between 2026-07 and 2026-08). Treat every Fab price as "lowest tier (Personal) price in the crawler's region/currency, re-verify live before buying". Most cards were USD; the KitBash3D seller page on Fab showed EUR (EUR 271.98), so currency/VAT is region-dependent. Nothing was purchased, downloaded or tested; every quality, Nanite, collision and "playable" claim below is ADVERTISED by the seller unless stated otherwise. Fab ratings for the premium packs are based on very small review counts (often 3-25), so ratings are weak evidence.

## A. Which Unreal Engine version is current, and how do listings show version support?

### Takeaway
UE 5.8 (released 2026-06-17) is the current stable line, now at hotfix 5.8.3 (2026-09-22). Epic calls 5.8 the last planned major UE5 release (a 5.9 is only a possibility), so 5.8 is the right compatibility target. Fab/KitBash listings show a seller-declared "Supported Unreal Engine Versions" range, and many strong city/hotel packs still stop at 5.7 or earlier, so a 5.8 project will often mean "upgrade the pack yourself and test".

### Cited Findings
- Epic announced "Unreal Engine 5.8 is now available" on 2026-06-17 (Launcher, GitHub, Linux) — [Epic forum announcement](https://forums.unrealengine.com/t/unreal-engine-5-8-released/2729274)
- Epic: "UE 5.8 is the last planned major Unreal Engine 5 release on our roadmap as we ramp up work on UE6. We will continue to support UE5 for bug fixes and regressions, and may add another official release if circumstances warrant it." Some 5.8 features are Beta/Experimental — [Epic: Unreal Engine 5.8 is now available](https://www.unrealengine.com/news/unreal-engine-5-8-is-now-available)
- Hotfixes: 5.8.1 (2026-07-28), 5.8.2 (2026-08-25), 5.8.3 (2026-09-22); as of 2026-09-24 the commentary says "plan tools against 5.8 and don't wait for a 5.9" — [Simstim UE news journal (secondary)](https://simstim.org/journal/unreal-news); 5.8.2 confirmed by [Epic forum hotfix post](https://forums.unrealengine.com/t/5-8-2-hotfix-released/2746335)
- 5.8 headline features (MegaLights production-ready, Lumen Lite, experimental Mesh Terrain, experimental Unreal MCP server plugin, PCG city/street attributes, Procedural Vegetation Editor) — [GameFromScratch 2026-06-17 (secondary)](https://gamefromscratch.com/unreal-engine-5-8-released/)
- A UE 5.7 to 5.8.3 upgrade report on a large World Partition landscape with LandscapeSplineActors shows heavy PIE memory growth after moving to 5.8 (user report; relevant if road/spline-heavy city assets are used) — [Epic forum bug thread, 2026-09-22](https://forums.unrealengine.com/t/ue-5-8-3-hotfix-bug/2833385)
- How Fab shows version support (examples): "Supported Unreal Engine Versions 5.3 – 5.7" on [Beach City – Modular Pack](https://www.fab.com/listings/c90bc1f1-3d5d-469e-a1d3-df2b439753f0); "4.19 – 4.27 and 5.0 – 5.7" on [Modern City Downtown Megapack](https://www.fab.com/listings/e6bae9e3-10eb-4f9f-aa93-c09608e782f9); "4.25 – 4.27 and 5.0 – 5.8" on [Downtown – City Pack (PolySphere)](https://www.fab.com/listings/bf3d8286-25e2-4cb5-a085-79312ea0c144) even though that listing states "Last Update: June 2023"; a 2026 texture pack explicitly sells "Unreal Engine 5.8 material instances" — [Fab hotel-lobby PBR materials listing](https://www.fab.com/listings/f313b11e-65f5-4821-bb34-ff589863b53e?lang=fr)
- KitBash3D Gameplay Ready kits are "optimized for Unreal Engine 5.2 and above" and use Nanite, Packed Level Instances and a USD importer — [KitBash3D Gameplay Ready page](https://kitbash3d.com/pages/gameplay-ready-kits)
- Quixel: new Megaplants will be compatible with UE 5.8 and above only; existing Megaplants were converted for 5.8 — [Quixel news 2026-07-16](https://quixel.com/news/discover-the-latest-quixel-megascans-and-free-megaplants)
- Evermotion UE scenes are tagged for specific versions (for example "Unreal Engine 5.3.2, 5.4" for Archinteriors vol. 12 scenes) — [Evermotion Scene 5 AI12](https://evermotion.org/shop/show_product/scene-5-ai12-for-ue-archinteriors/19916)

### Inferences
- A displayed range such as "5.3 – 5.7" is a seller claim, and a range reaching "5.8" on an old listing (PolySphere, last update June 2023) may only mean the label was extended, not that the pack was retested; treat as unproven until opened in a 5.8 project.
- Because UE 5.8 is the end of the UE5 line, buying packs now carries less future version-churn risk than in earlier years.

### Gaps
- Fab's own documentation page on how version ranges are set/verified was not accessible (dev.epicgames.com docs return 403 through the crawler), so whether Epic validates the range is UNVERIFIED.
- No evidence found of any actual 5.9 release or preview as of 2026-10-09 (absence of results, not proof).

## B. Quixel Megascans: current status inside Fab, and is it still free?

### Takeaway
No, Megascans are no longer broadly free. The "everything free" window ended 2024-12-31. Since 2025-01-01 Megascans are sold on Fab at roughly $0.99-$4.99 per asset (plus packs), with a free tier (announced as 1,500+ assets) and free Megaplants, all under the Fab Standard License. Quixel is actively adding premium content in 2026, including an urban pack relevant to this project.

### Cited Findings
- Epic's Sept 2024 plan: all Megascans free on Fab until end of 2024 under Fab Standard License; from 2025 dollar pricing: standalone 2D/3D assets from $0.99, procedural kits from $4.99, asset packs from $24.99 — [Epic forum: Upcoming changes to Quixel](https://forums.unrealengine.com/t/upcoming-changes-to-quixel-related-to-fabs-launch-in-october/2024362); same numbers in [Quixel to Fab Transition FAQs](https://forums.unrealengine.com/t/quixel-to-fab-transition-faqs/2033839)
- "The Megascans library on Fab.com will no longer be free after December 31, 2024 ... Beginning January 1, 2025, we will offer a selection of 1500+ Megascans assets for free on Fab.com ... under the Fab Standard License"; legacy Quixel.com/Bridge library no longer acquirable after 2024-12-31 (previously claimed assets stay accessible) — [Epic forum: Reminder, free Megascans ends soon (2024-12-11)](https://forums.unrealengine.com/t/reminder-free-megascans-ends-soon/2203090)
- Anything claimed during the 2024 promo is kept "even after this promotion ends" under the Fab Standard License — [Quixel to Fab Transition FAQs](https://forums.unrealengine.com/t/quixel-to-fab-transition-faqs/2033839)
- Quixel.com now states the way to get Megascans is Fab.com or Fab in the Epic Games Launcher; the legacy library only gives previously acquired items — [quixel.com/license](https://quixel.com/license)
- Quixel says it is "ramping up" Megascans drops on Fab; new premium releases since March 2026: Neo Renaissance Building, Abandoned Factory, Pakistan Street, Old Desert Ruins, and Urban Megadrop (over 600 assets). Megaplants are "free to use under the Fab Standard License" and "fully Nanite-compatible" (advertised). Quixel claims "standardized topology, UVs, LODs, and PBR materials" (advertised) — [Quixel news 2026-07-16](https://quixel.com/news/discover-the-latest-quixel-megascans-and-free-megaplants)
- Fab promoted the Urban Megadrop on 2026-06-22 ("buildings, infrastructure and everything in between") — [Fab LinkedIn post](https://www.linkedin.com/posts/fabofficial_take-a-city-break-to-quixels-urban-megadrop-activity-7474854028854575105-tJOM)
- Observed Fab card prices (cached crawl, date not shown) for Urban Megadrop-type items: Urban Modular Building pieces From $2.99; Modular Curb From $2.99; Sidewalk Tiles From $0.99; Asphalt Road From $0.99; Fine Road From $0.99; Urban Street Cobblestone Granite From $1.99; Urban Trash "From Free" — [Fab search: Quixel Urban Megadrop](https://www.fab.com/search?q=Quixel%20Urban%20Megadrop); [Fab search: Neo Renaissance Building](https://www.fab.com/search?q=Neo%20Renaissance%20Building%20Quixel). Plant/rock items From $4.99; format tags "fbx glb +2" — [Fab seller page Quixel Megascans](https://www.fab.com/sellers/Quixel%20Megascans)
- Fab homepage shows additional Megascans cards marked "From Free" — [Fab homepage](https://www.fab.com/)
- Fab in Launcher (Sept 2025) is "Fab's first step towards eventually replacing Quixel Bridge"; some legacy assets "are still in the process of being migrated ... and others will not be" because they no longer meet quality standards — [Quixel news 2025-09-15](https://quixel.com/news/fab-in-launcher-brings-quixel-bridge-features-to-the-epic-games-launcher)
- Account-linked entitlements: you must use the same Epic account and accept the Fab EULA; free-plan/free-trial claims made before 2024-10-22 were NOT auto-migrated to Fab — [Epic forum: Megascans update March 2025](https://forums.unrealengine.com/t/megascans-update-march-2025/2419561)
- Conflicting/secondary: some 2026 blog summaries say Megascans are "free" or "free tier only"; these were not verifiable against Epic and conflict with the official pricing above (treat as unreliable) — surfaced by general web search, no single primary source.

### Inferences
- For this project Megascans are best used as a cheap per-asset supplement ($0.99-$4.99) for surfaces, curbs, road materials, trash/debris and scanned props, not as a free complete library; budget perhaps tens of dollars for a focused urban kit rather than thousands.
- If the developer's Epic account claimed everything in 2024, those items remain in the Fab library (cannot be done retroactively now).

### Gaps
- Pack-level price of Urban Megadrop / Neo Renaissance Building packs UNVERIFIED (the search cards only showed per-asset prices; "pack from $24.99" is the 2024 policy figure).
- Current size of the free tier (1,500+ was the Dec 2024 announcement) UNVERIFIED for Oct 2026.
- Whether Megascans source meshes arrive as native .uasset (via Fab-in-UE) versus FBX/GLB: Fab cards show fbx/glb tags and Epic says Fab integrates in UE 5.3+; exact import fidelity (Nanite flags, LOD set) UNVERIFIED without testing.

## C. Licensing gate for a commercial Steam release (cross-marketplace)

### Takeaway
Fab Standard License, CC0 (Poly Haven, ambientCG) and KitBash3D perpetual kit licenses are all clearly usable in a shipped commercial Steam game when assets are cooked into the game. The traps are: editorial-labelled items (TurboSquid/CGTrader/Sketchfab — not usable in games), ArtStation's Standard license (capped at 2,000 sales), KitBash3D individual plans (single-person only; subscription lapse blocks new-project use), Poliigon's bot/API/AI terms, public-repo sharing, and pirate mirror sites that re-host paid Fab packs.

### Cited Findings
- Fab Standard License summary: commercial or private use; modify; commercially distribute your Projects with assets incorporated; use in any compatible tool (not limited to UE); share with collaborators "directly, via a private repository or in the Project"; may not resell/redistribute the asset standalone; Personal and Professional tiers grant "the same scope of rights"; no need to upgrade tier after purchase if revenue later passes the threshold; no credit required. The summary is "not legally binding"; the full EULA governs — [Fab Standard License](https://www.fab.com/eula)
- Tier eligibility text (EULA section 2a, as quoted by forum users and consistent with the page above): Personal / Personal-Reference-Only only if you and affiliates have not generated more than $100,000 USD gross revenue from commercial activity in the digital content industry in the last 12 months. The EULA summary says the Reference-Only tier gives access only to a referenced asset, not its source format (so it is not a shippable-source tier; a forum user adds it is not meant for commercial products) — [Epic forum quoting EULA 2a](https://forums.unrealengine.com/t/using-free-fab-ue-marketplace-skeletal-meshes-and-animations-in-a-commercial-project/2259858), [Fab Standard License](https://www.fab.com/eula)(https://forums.unrealengine.com/t/using-free-fab-ue-marketplace-skeletal-meshes-and-animations-in-a-commercial-project/2259858)
- Epic staff: Limited-Time Free Program items are available under the Fab Standard License; UE Marketplace "Free For the Month" items from before remain under the older Epic Content License — [Epic forum](https://forums.unrealengine.com/t/fab-limited-time-free-assets-licences-personal-or-professional/2305601)
- Fab only offers CC BY and Standard licenses (no CC0, CC BY-NC, BY-SA, BY-ND) — [Epic staff reply, Fab forum](https://forums.unrealengine.com/t/non-commercial-models/2027604)
- Fab listings carry fields "Allows usage with AI" and "Generated with AI" (Yes/No); AI-generated content is allowed on Fab but must be declared — [GameAssetly Fab checklist, 2026-10-06 (secondary)](https://gameassetly.com/fab-asset-buying-checklist/); example field values on listings: "Allows usage with AI: No" on [The Projects – Modular City Pack](https://www.fab.com/listings/173d12c0-fead-4f89-aa6f-e85214406d29?lang=en) and [Realistic City – San Francisco Deluxe](https://www.fab.com/listings/bb18a4ef-fddd-44bc-9f9b-6c7661d623de?lang=en)
- Poly Haven: all assets CC0; use for any purpose incl. commercial, no credit, may redistribute or include in a product you sell; website ToS forbids scraping/data mining without permission — [Poly Haven license](https://polyhaven.com/license)
- ambientCG: all assets CC0; "You can include the raw files in your project, for example a video game" — [ambientCG license](https://ambientcg.com/license)
- KitBash3D: subscription and perpetual licenses both allow commercial use incl. games and "Publish & Monetize Your Work"; subscription users must keep the plan active to use assets in new projects; Kits bought one-time carry a perpetual licence that "can't be resold or redistributed on their own" — [KitBash3D licenses](https://kitbash3d.com/pages/licenses), [KitBash3D agents.md](https://kitbash3d.com/agents.md)
- KitBash3D Individual plans are only for independent artists/single-owner entities; "If your organization ... has two or more employees, owners, or contractors, you must use a Teams plan" (Teams from $99/user/month) — [KitBash3D Subscriptions Overview, 2026-03-31](https://help.kitbash3d.com/en/articles/7833662-subscriptions-overview), [KitBash3D pricing](https://kitbash3d.com/pages/pricing)
- KitBash3D single-Kit license tiers: Individual (sole proprietors, no employees), Small Business (up to 50), Enterprise (51+) — [KitBash3D Paris product page](https://kitbash3d.com/products/paris)
- TurboSquid Royalty Free License permits Computer Games (incl. mods, MMOs) if assets stay in an interactive experience, in proprietary formats not openable/extractable, and the game has no import/export functionality for 3D models; "Editorial Use Only" models cannot be used in video games (commercial use) and cannot be upgraded — [TurboSquid Royalty Free License](https://www.turbosquid.com/help/en/articles/9937422-royalty-free-license), [Royalty Free License FAQ](https://www.turbosquid.com/help/en/articles/9937423-royalty-free-license-faq), [Editorial Use Information](https://www.turbosquid.com/help/en/articles/9937424-editorial-use-information)
- CGTrader Royalty Free License: use limited to "Incorporated Product" (inside a proprietary game format, not user-extractable); no resale of the raw product; a separate "Royalty Free License, No AI" variant forbids ML/AI-training use while the plain Royalty Free license explicitly permits it; Editorial-licensed items are limited to editorial/newsworthy uses — [CGTrader Terms and Conditions sec. 21A/21B/22](https://www.cgtrader.com/pages/terms-and-conditions), [CGTrader Editorial License](https://help.cgtrader.com/hc/en-us/articles/360015193398-Editorial-License)
- Sketchfab: downloadable CC models require credit and Sketchfab says "more than 700,000" are downloadable under CC; CC options include BY-NC, BY-ND, BY-SA variants (NC forbids commercial use) — [Sketchfab Download API guidelines](https://sketchfab.com/developers/download-api/guidelines), [Sketchfab CC introduction](https://sketchfab.com/blogs/community/an-introduction-to-creative-commons-licenses/). Sketchfab's legacy purchased-license agreement forbids promoting "gambling, weapons or explosives" with the Licensed Material and bars editorial items from any commercial use — [Sketchfab License Agreement](https://sketchfab.com/licenses)
- ArtStation Marketplace stock-asset licences: Standard = one commercial Work up to 2,000 sales or 20,000 monthly views; Extended = any number of commercial Works with no cap; sellers may substitute a custom EULA — [ArtStation Help: EULAs (2026-08-07)](https://help.artstation.com/en/articles/16155014-eula-end-user-license-agreements), [ArtStation Marketplace Product EULA](https://www.artstation.com/marketplace-product-eula)
- Evermotion: product pages list a Standard licence for "individual creators or small teams with up to $1 million in revenue or funding (last 12 months)" and a Professional licence above that — [Evermotion Scene 2 AI12](https://evermotion.org/shop/show_product/scene-2-ai12-for-ue-archinteriors/19913)
- Poliigon: assets royalty-free incl. games, but may not be used in games/software where users get direct or indirect access to assets, nor in game mods/maps; AI/ML use of assets is prohibited without a separate licence; API use needs written consent; "scripts or bots to automate downloads" are prohibited on Unlimited plans — [Poliigon Asset Use & Licensing](https://help.poliigon.com/en/articles/8749749-asset-use-licensing), [Poliigon Terms](https://www.poliigon.com/terms), [How Unlimited plans work](https://help.poliigon.com/en/articles/10003983-how-unlimited-plans-work)
- Unity Asset Store assets "are not restricted to Unity projects" and can be used with other engines if the Asset Store EULA is followed (must be incorporated with substantial original content, no extractable redistribution, Restricted Assets/per-asset EULAs override) — [Unity Support](https://support.unity.com/hc/en-us/articles/34387186019988-Can-I-use-assets-from-the-Asset-Store-with-other-engines), [Unity Asset Store EULA](https://unity.com/legal/as-terms-legacy)
- Pirate re-hosts appear in search results for paid Fab packs (observed: assetsue.com and psdly.co.uk re-hosting PropHaus City Streets, and assetsue.com re-hosting Tennur and Env-Art hotel packs). These redistribute paid assets in violation of the Fab Standard License ("may not resell or redistribute the asset ... on a standalone basis") and must not be used; they are named here only so the report writer can warn against them (no links, do not visit) — observed in Exa search results 2026-10-09.

### Inferences
- Because Fab's EULA allows sharing with collaborators only "directly, via a private repository or in the Project", AI-agent workflows that commit assets to git must use a private repo (Git LFS/Perforce) and never a public repository or public CI artifact.
- A solo dev under $100k trailing revenue qualifies for the Personal tier; both tiers grant identical rights, so the cheaper Personal price is the right one to buy.
- Any item labelled "Editorial" on TurboSquid, CGTrader or Sketchfab is automatically disqualified for the game; this includes branded luxury hotel/casino items (a TurboSquid "Luxury Hotel Lobby Interior" listing mirrored on cghub.one carries "Editorial Uses Only" and shows the usage matrix with "PC Game development" unchecked) — [CGHub mirror listing](https://cghub.one/item/luxury-hotel-lobby-interior)
- Real-world brand marks inside purchased packs (signage, logos) are an IP risk independent of the marketplace licence; prefer "generic/invented patterns only" packs (as one hotel texture pack states) or remove branding.

### Gaps
- The full Fab EULA body text (sections incl. AI clauses) could not be retrieved; only the summary and forum-quoted clause 2a were read. The dev.epicgames.com "Licenses and Pricing in Fab" doc returned 403.
- Evermotion's full licence text on use in games was not retrievable (licence page fetch failed): game use is UNVERIFIED.
- Whether any seller on Fab prices the Professional tier much higher than Personal (forum anecdote of large gaps) is UNVERIFIED and matters only if revenue exceeds $100k.

## 1. Findings per marketplace (evidence + URLs)

### Fab (fab.com) — primary channel

#### Takeaway
Fab is the only place that combines UE-native packs (.uasset projects), Megascans, KitBash3D kits, and in-editor integration, with the widest range of complete environments. Quality varies enormously; the best packs are large modular city kits from experienced publishers, the weakest are cheap single-room "hotel/casino" packs. Verify each listing manually.

#### Cited Findings
- UE channel page advertises "60,000+ assets built for UE", with filters for Nanite, Lumen, PCG, Level, etc., and is also available inside the editor and Launcher — [Fab Unreal Engine channel](https://www.fab.com/channels/unreal-engine)
- Fab is the successor to the UE Marketplace and Sketchfab Store and the home of Megascans; ArtStation Marketplace was planned to migrate in 2025 — [Epic: Fab launches](https://www.epicgames.com/site/en-US/news/fab-epics-new-unified-content-marketplace-launches-today), [Unreal Engine blog](https://www.unrealengine.com/en-US/blog/fab-content-marketplace-launches-in-october-publishing-portal-opens-today). As of Aug 2026 ArtStation's own marketplace still operates (see ArtStation below) and Epic retained Fab — [Gaming.net](https://www.gaming.net/epic-games-sells-artstation-and-sketchfab-as-kitbash-takes-over-artist-platforms/)
- Free content: Fab "Limited-Time Free" program; in Oct 2026 the bi-weekly giveaway (Oct 6-20) is "Ready Map Vol 2 – Bridge Environment / Nature", "AdaptivePathfindingSystem: AI Navigation Plugin" and "Stylized Harvest Farm Environment" — [GameFromScratch October 2026 round-up](https://gamefromscratch.com/october-free-gamedev-assets-round-up/)
- Search cards show many luxury-hotel/casino/mansion/office packs priced low (see price evidence below). Ratings are mostly based on 1-8 reviews, so quality evidence is thin — [Fab search: luxury hotel](https://www.fab.com/search?q=luxury%20hotel&channels=unreal-engine), [casino](https://www.fab.com/search?q=casino&channels=unreal-engine), [modern mansion interior](https://www.fab.com/search?q=modern%20mansion%20interior&channels=unreal-engine), [office/skyscraper](https://www.fab.com/search?q=photoreal%20office%20skyscraper%20interior%20modular&channels=unreal-engine)
- Hotel/casino/mansion price evidence (From, lowest tier, USD): Hotel Lobby and Bar – Modular Interior (CrescentDialGames) $19.99 [listing](https://www.fab.com/listings/fc4a4c2c-1874-486b-8bac-e8d31cf749a9?lang=en); Hotel Lobby Environment, 138 unique meshes (Tennur) $69.99 [listing](https://www.fab.com/listings/00739a3c-1dba-4754-bb12-94f43eb78019?lang=en); Modern Hotel Lobby (MonkeyRED) $99.99; Modular Hotel Corridor (Env-Art Studio) $49.99 (4.3, 3 ratings); Underwater Art Deco Hotel Environment (Leartes) $69.99; Modular Casino (Macbry) $74.99; Monte Carlo Style Casino (Nimikko) $39.99; Casino Environment (CGStuff Studios) $59.99 (Mature); SICKA MANSION $49.99 (4.6, 80 ratings); Grand Palace – Majestic Mansion (Barrel Bark Interactive) $59.99 (4.0, 8); Modern Interiors Mega Bundle $349.99 — cards in the searches linked above
- Military/industrial price evidence: Military Base Megapack (Leartes, 4.8 from 36) $249.99; Military Base & Camp Environment 4km (FreshCan, 4.9 from 14) $79.99; Military Base Megapack (Hivemind, 5.0 from 7) $99.99; Dekogon "Meridian 1988: Beacon Freight & Repair – Modular Environment (VOL 24)" $109.99 (Mature) — [Fab search: military base](https://www.fab.com/search?q=military%20base%20realistic&channels=unreal-engine), [Dekogon seller page](https://www.fab.com/sellers/Dekogon%20Studios)
- Stealth-genre templates exist but are overlap with the gameplay-framework specialist: Tactical Stealth Operation $55.99 (4.5 from 2), Stealth Action Game Template (EazySoft) $56.99, 1011's Stealth System $29.99 (4.7 from 13) — [Fab search: stealth game template](https://www.fab.com/search?q=stealth%20game%20template&channels=unreal-engine)
- Playable vs showcase: sellers commonly state included demo maps. Examples (advertised): San Francisco Deluxe "Fully playable environment included ... Over 1 km x 1 km" with 10 simple-interior buildings and working door blueprints; The Projects includes a World Partition demo level with 25+ city blocks and "1500 unique ... Nanite static meshes"; PropHaus City Streets/Beach City include prebuilt level-instance buildings and a demo map and use Nanite+Lumen; PolySphere Downtown lists "Collision: Yes. LODs: Yes" — [SF Deluxe](https://www.fab.com/listings/bb18a4ef-fddd-44bc-9f9b-6c7661d623de?lang=en), [The Projects](https://www.fab.com/listings/173d12c0-fead-4f89-aa6f-e85214406d29?lang=en), [City Streets](https://www.fab.com/listings/8b29843a-a98d-4f86-9c3f-8ec9b511347a?lang=de), [PolySphere Downtown](https://www.fab.com/listings/bf3d8286-25e2-4cb5-a085-79312ea0c144)
- Typical caveats stated by sellers themselves: interiors are often partial ("Most buildings include interior support" for City Streets; The Projects and SF Deluxe offer interiors only in some buildings), and city packs generally do not include characters, drivable cars or traffic ("this pack does not contain any Metahuman assets, pedestrians, drivable vehicles, or traffic") — [City Streets PropHaus forum thread](https://forums.unrealengine.com/t/prophaus-city-streets-modular-pack/2426163), [SF Deluxe](https://www.fab.com/listings/bb18a4ef-fddd-44bc-9f9b-6c7661d623de?lang=en)
- Platform caution: Fab packages are mostly UE-native; some listings supply FBX/3ds Max too (PolySphere "Unreal Engine, 3ds Max, fbx"); format badges are shown under "Included formats" — [PolySphere Downtown](https://www.fab.com/listings/bf3d8286-25e2-4cb5-a085-79312ea0c144)

#### Inferences
- Nothing found on Fab is a verified drop-in "luxury hotel" at GTA V Enhanced or Hitman level; the most credible route is modular city/architecture kits plus hero props, Megascans surfaces and custom lobby dressing.
- Cheap single-purpose hotel/casino packs ($20-$100) are good for blockout and secondary rooms; assess lighting/material quality in a 5.8 project before committing.
- Fab "From" pricing is the Personal tier; check whether a Professional tier is shown only if the developer expects over $100k revenue.

#### Gaps
- No collision/LOD/Nanite inspection performed for any pack (advertised only).
- Many cards omit publisher names or ratings in the crawl; product URLs for some cards (Modern Hotel Lobby, Modular Casino, Grand Palace) were not captured.

### Quixel Megascans / Megaplants (inside Fab) — scanned surfaces, props and vegetation

#### Takeaway
Highest-fidelity scanned library available for UE; now paid per asset with a free tier and free Megaplants. Best "essential" supplement for sidewalks, asphalt, concrete, brick, trash, debris and vegetation. See section B for evidence.

#### Cited Findings
- See section B; additionally the Fab channel page lists a "Quixel: Megascans & Megaplants" shelf with items from $2.99 and Free — [Fab Unreal Engine channel](https://www.fab.com/channels/unreal-engine)
- Megascans on Fab are under the Fab Standard License (usable in any engine) — [Fab Standard License](https://www.fab.com/eula)

#### Inferences
- Scanned assets are photoreal but expensive on texture memory/geometry; a solo dev should rely on Nanite and keep texture budgets in check (general engine knowledge, not from a cited source).

#### Gaps
- Whether Megaplants (UE 5.8+ only for new releases) work for city street trees without PVE experimental features UNVERIFIED.

### Poly Haven (polyhaven.com) — free CC0 HDRIs, textures, models

#### Takeaway
Zero-risk licensing and genuinely photoreal, but small: roughly 2,370 assets (about 520 models). Use it for HDRIs (sky/interior lighting), tileable materials and a few props; it cannot supply complete environments.

#### Cited Findings
- CC0 for everything, commercial use with no attribution — [Poly Haven license](https://polyhaven.com/license)
- Public API lists 521 models, 856 texture sets, 993 HDRIs as of Sept 2026; formats glTF/Blend/FBX; 4k default, 8k for most textures; no-key API — [Cinevva comparison guide, 2026-06-23 (secondary, cites API counts)](https://app.cinevva.com/guides/sketchfab-polyhaven-kenney); [Poly Haven stats](https://polyhaven.com/stats) confirms default 4k / max resolutions
- Over 3,100 active Patreon supporters (Jan 2026); supporters get early access — [Poly Haven Patreon](https://www.patreon.com/posts/studio-hdri-is-149482595)
- Models list is exposed via API (api.polyhaven.com/assets?type=models) — [Poly Haven models page](https://polyhaven.com/models)

#### Inferences
- The no-key API makes Poly Haven the easiest library for an AI coding agent to script downloads from, but its Terms of Service forbid scraping; use the documented API, not crawlers.

#### Gaps
- Poly Haven model categories relevant to urban/hotel use (furniture, props) not enumerated; model polycounts/LODs/collision UNVERIFIED.

### ambientCG (ambientcg.com) — free CC0 PBR materials

#### Takeaway
Excellent free CC0 material source (floors, plaster, marble-like surfaces, paving). Materials only plus some HDRIs/models; no environments.

#### Cited Findings
- Homepage: "All assets are released under the Creative Commons CC0 license ... free to use without attribution - even in commercial circumstances"; offers textures, HDRIs, models; currently features the "Borkum Collection" of scans and HDRIs — [ambientCG home](https://ambientcg.com/)
- License allows including raw files in a video game — [ambientCG license](https://ambientcg.com/license)

#### Inferences
- Pair with Poly Haven for tileable base materials and with Megascans for hero surfaces.

#### Gaps
- Total asset count, API availability, and UE import tooling UNVERIFIED.

### KitBash3D (kitbash3d.com, Cargo) — premium architecture kits, now UE "Gameplay Ready"

#### Takeaway
Strongest verified claims of game-readiness among architecture kit vendors: Gameplay Ready kits advertise Nanite support, handcrafted collision (<255 tris), Packed Level Instancing and modular Blueprints. Perpetual single-kit licences (about $245) are the right purchase for a long-lived game; subscriptions are a trap for projects that outlive the subscription, and Individual plans cannot be used by anyone with a co-founder/contractor.

#### Cited Findings
- Gameplay Ready: full Nanite support (translucents split out), handcrafted collisions under 255 tris, Packed Level Instancing, "walkable, playable worlds", claimed 82% draw-call and 84% mesh-memory reduction (vendor figures); optimized for UE 5.2+; import via Cargo USD importer — [Gameplay Ready page](https://kitbash3d.com/pages/gameplay-ready-kits), [KitBash3D blog 2025-07-15](https://kitbash3d.com/a/blog/introducing-gameplay-ready-kits-built-for-real-time-performance-in-unreal-engine)
- Initially 12 kits re-released (sci-fi/fantasy plus two free kits); "every future Kit we release will be Gameplay Ready out of the box" (vendor statement, July 2025) — same blog
- Kits restructure in UE: buildings as Blueprints of modular assets; 4K kits default to 2K via texture LOD bias 1; translucent glass separated — [KitBash3D help: Structural Updates, 2025-08-19](https://help.kitbash3d.com/en/articles/11698511-structural-updates-to-unreal-engine-kits-gameplay-ready)
- Interiors: "Interior-Ready World Kits" (all kits after Hong Kong; upgraded Neo San Francisco, Outlaw, Shogun, Cyberpunk) have semi-transparent windows with modeled internal floors/walls but are not furnished; dedicated Interiors kits exist (Apartment Interiors, Retail Stores, Neighborhood Shops, Restaurants, Police Department, Emergency Response, Cyberpunk Interiors); legacy kits are hollow exteriors — [KitBash3D help: Understanding Interiors, 2026-08-25](https://help.kitbash3d.com/en/articles/2445183-understanding-interiors-in-kitbash3d-kits), [What comes with a Kit, 2026-06-25](https://help.kitbash3d.com/en/articles/6449589-what-comes-with-a-kit)
- Relevant kit: Paris, $245, Gameplay READY, Haussmann buildings, Luxury Shops, Metro Station, Train Station, City Hall, Coffee Shop, bridge, street lamps, bus stop, signs; Nanite, custom collisions, Packed Level Instancing; designed for 6-core CPU, 8 GB VRAM, 32 GB RAM, 15 GB disk per kit — [KitBash3D Paris](https://kitbash3d.com/products/paris)
- Relevant kit: Neo San Francisco, $245, 142 models/31.1M polys, includes "The Grand Hotel", "Hotel Cali", "The Luxe", "Costal Marina Hotel", "Luxury Apartments", Convention Center; futuristic/solarpunk art direction ("next-generation high rises ... holograms") — [KitBash3D Neo San Francisco](https://kitbash3d.com/products/neo-san-francisco)
- Relevant kit: Hong Kong, $245, 322 models/34.4M polys, skyscrapers, office/condo towers, alleys, construction sites, vendor props — [KitBash3D Hong Kong](https://kitbash3d.com/products/hong-kong)
- Cargo: free to download with "100+ free assets", one-click send to Unreal/Blender/etc.; subscription KitBash3D Library $119/month or $708/year ($59/month equivalent), Everything Bundle $159/month or $948/year; Teams from $99/user/month — [KitBash3D agents.md](https://kitbash3d.com/agents.md), [KitBash3D pricing](https://kitbash3d.com/pages/pricing)
- KitBash3D kits also sell on Fab (for example Neo San Francisco From $249.99; Fab seller page shows EUR 271.98 for many kits in a EUR crawl) — [Fab search: SF city](https://www.fab.com/search?q=Realistic%20City%20San%20Francisco%20Deluxe), [Fab KitBash3D seller](https://www.fab.com/sellers/KitBash3D)
- Ownership change: KitBash (KitBash3D + Greyscalegorilla) acquired ArtStation and Sketchfab from Epic on 2026-08-10; KitBash says nothing changes for KitBash3D/Cargo customers and no roadmap changes announced — [Sketchfab blog](https://sketchfab.com/blogs/community/kitbash-acquires-sketchfab-and-artstation/), [Gaming.net](https://www.gaming.net/epic-games-sells-artstation-and-sketchfab-as-kitbash-takes-over-artist-platforms/)

#### Inferences
- KitBash3D is the best-documented, best-optimized source of hero architecture for a modern-city espionage setting, particularly Paris and Hong Kong (neutral contemporary tone) and Neo San Francisco's hotel buildings (if the futuristic tone is acceptable or retextured).
- Single kit purchases are cheaper than a year of subscription if only 1-3 kits are needed ($245 each vs $708/year).
- Whether Paris, Hong Kong and Neo SF are fully "Gameplay Ready" versus only "Interior-ready/upgraded importer" should be confirmed in the product's Unreal files; the Paris page explicitly carries the Gameplay READY badge.

#### Gaps
- Kit-level hotel interiors (furnished lobbies, suites) were not found in KitBash3D's list; interior kits listed are apartment/retail/restaurant/police/cyberpunk. A luxury-hotel interiors kit is UNVERIFIED as existing.
- Whether the Hong Kong kit has the Gameplay Ready badge UNVERIFIED (page text did not show it; help text says kits "after Hong Kong" are interior-ready).
- Small Business tier price for kits UNVERIFIED (only the $245 Individual price was shown).
- Modern-city collection page was not retrievable (404), so the full list of modern-city kits is incomplete.

### Sketchfab (sketchfab.com) — CC model library, store closed

#### Takeaway
Useful for hunting individual props and scans under CC0/CC-BY, but not a source of production packs. License discipline is per model and attribution burdens apply. The paid store is gone (moved to Fab); the site changed owner in Aug 2026 (KitBash).

#### Cited Findings
- Sketchfab Store closed when Fab launched (2024-10-22); store models were migrated to Fab (CC BY or Standard only; CC0/BY-SA/BY-NC/BY-ND could not migrate) — [Sketchfab blog](https://sketchfab.com/blogs/community/sketchfab-update-what-you-need-to-know-now-that-fabs-live/), [Fab migration post](https://sketchfab.com/blogs/community/fab-publishing-portal-open-for-sketchfab-migration/)
- Epic had warned free Sketchfab download licensing could be removed "next year" (2025) after Fab offered a free-only signup; as of 2026-08-10 Sketchfab states "You can continue to share and download models on Sketchfab like you have before" and no pricing or plan changes — [Sketchfab blog 2026-08-10](https://sketchfab.com/blogs/community/kitbash-acquires-sketchfab-and-artstation/)
- Sketchfab says 700,000+ models are downloadable under CC; formats glTF, FBX, OBJ; login required (secondary comparison) — [Sketchfab Download API guidelines](https://sketchfab.com/developers/download-api/guidelines), [Cinevva](https://app.cinevva.com/guides/sketchfab-polyhaven-kenney)
- Editorial-licensed assets cannot be used commercially — [Sketchfab License Agreement](https://sketchfab.com/licenses)

#### Inferences
- For attribution-requiring CC BY models used in a Steam game, plan an in-game credits screen; filter out NC/ND/SA entirely.
- Photogrammetry scans on Sketchfab are typically uncleaned high-poly meshes needing retopology/baking; quality not assessed.

#### Gaps
- Current exact policy on free-model download licensing beyond the Aug 2026 statement UNVERIFIED; whether Sketchfab's Download API is usable for scripted agent downloads (rate limits, ToS) UNVERIFIED.

### ArtStation Marketplace (artstation.com/marketplace)

#### Takeaway
Still operating in Oct 2026 (the planned 2025 migration to Fab did not remove it) and now owned by KitBash. Catalog is artist-driven and mixed in quality with AI-content concerns; its Standard licence has a 2,000-sales cap that a Steam game could exceed, so only Extended or custom-EULA purchases are suitable.

#### Cited Findings
- Standard EULA: one commercial Work up to 2,000 sales / 20,000 monthly views; Extended: unlimited — [ArtStation Help: EULAs, updated 2026-08-07](https://help.artstation.com/en/articles/16155014-eula-end-user-license-agreements)
- ArtStation said after the 2026-08-10 sale: users "can still ... buy and sell 2D and 3D assets and prints" — [GamesBeat](https://gamesbeat.com/kitbash-acquires-artstation-and-sketchfab-art-platforms-from-epic-games/)
- ArtStation ToS require "CreatedWithAI" tagging for AI-generated products; press coverage notes community complaints about AI content in the marketplace — [ArtStation ToS](https://www.artstation.com/tos), [Kotaku 2026-08-12](https://kotaku.com/epic-artstation-fortnite-unreal-sold-sketchfab-kitbash3d-2000723775)
- Epic's 2024 plan was for ArtStation Marketplace to migrate to Fab in 2025 — [Unreal Engine blog](https://www.unrealengine.com/en-US/blog/fab-content-marketplace-launches-in-october-publishing-portal-opens-today)

#### Inferences
- Not a primary source; use only for specific artists' packs not on Fab, and always pick Extended licence.

#### Gaps
- Marketplace catalog depth for photoreal UE hotel/city packs and formats was not examined (JS-rendered, not retrieved).

### TurboSquid (turbosquid.com) — FBX/OBJ single models, not UE-native

#### Takeaway
Royalty Free licence is game-compatible with conditions, but listings are DCC-format models (max/FBX/OBJ), not UE projects, quality is inconsistent, and many luxury interiors are archviz-heavy (millions of polygons) or Editorial-restricted. Prefer for individual hero props only.

#### Cited Findings
- License conditions for games (see section C) — [TurboSquid Royalty Free License](https://www.turbosquid.com/help/en/articles/9937422-royalty-free-license)
- Editorial models cannot be used in video games and cannot be upgraded — [Editorial Use Information](https://www.turbosquid.com/help/en/articles/9937424-editorial-use-information)
- Example: a $199 "Luxury Hotel Lobby Interior" with 8.3M polygons, flagged Editorial Uses Only and "PC Game development" not recommended — [CGHub mirror](https://cghub.one/item/luxury-hotel-lobby-interior)

#### Inferences
- High-poly archviz scenes require heavy conversion (Nanite helps, but UV/material/collision work is manual).

#### Gaps
- Typical TurboSquid prices for game-ready hotel/street assets and UE-specific filters were not retrievable (search pages blocked); price range UNVERIFIED beyond the single example.

### CGTrader (cgtrader.com) — FBX/GLB models, mixed quality

#### Takeaway
Cheap and wide, with game-ready labelled kits, but sellers are unvetted; read the licence line (Royalty Free vs Royalty Free No AI vs Editorial vs Custom) on every item.

#### Cited Findings
- Licence structure and Incorporated Product condition — [CGTrader Terms sec. 21A-22](https://www.cgtrader.com/pages/terms-and-conditions)
- Example listing: "Luxury Hotel Modular interior Kit": 32 assets, FBX and GLB, LOD1/LOD2 and collision proxies included, no animations, intended for Unreal/Unity (advertised; price not captured) — [CGTrader listing](https://www.cgtrader.com/3d-models/interior/other/luxury-hotel-modular-interior-kit)
- Other lobby listings are 3ds Max/V-Ray archviz scenes exported to FBX/OBJ, some mislabelled "rigged" — [CGTrader modern luxury hotel reception lobby](https://www.cgtrader.com/3d-models/interior/hall/modern-luxury-hotel-reception-lobby-3d-model)

#### Inferences
- A small number of listings may be useful, but there is no UE-native packaging; budget for import/material setup.

#### Gaps
- CGTrader price levels and quality distribution UNVERIFIED (search pages not retrievable).

### Evermotion (evermotion.org) — arch-viz quality scenes, Archinteriors for UE

#### Takeaway
Highest arch-viz fidelity of interior scenes, with UE-specific volumes at low prices (EUR 25-50 per scene, EUR 79-199 per volume/bundle), but version support listed up to UE 5.4 and archviz-oriented lighting; Hotel Lobby furniture bundles are not UE-native.

#### Cited Findings
- Archinteriors for UE vol. 17 (minimalist apartment) EUR 79; vol. 8 (modern apartments, 14 interiors, 1500+ assets) EUR 120; Archinteriors for UE Bundle vol. 2 (10 scenes) EUR 199; individual scenes EUR 25-50 — [Evermotion Archinteriors category](https://evermotion.org/shop/cat/397/archinteriors), [Bundle vol. 2](https://evermotion.org/shop/show_product/archinteriors-for-ue-bundle-vol-2/19172), [Scene 2 AI12](https://evermotion.org/shop/show_product/scene-2-ai12-for-ue-archinteriors/19913)
- vol. 12 scenes: Lumen, "all meshes without translucency are Nanite" in the UE 5.4 version, UE 5.3.2/5.4 listed — [Scene 5 AI12](https://evermotion.org/shop/show_product/scene-5-ai12-for-ue-archinteriors/19916)
- vol. 9 requirements: GeForce RTX 3060 recommended, 32 GB RAM strongly recommended — [Scene 5 AI9](https://evermotion.org/shop/show_product/scene-5-ai9-for-ue-archinteriors/19162)
- Hotel Lobby Furniture Bundle vol. 1 (Archmodels 296 + 298, 600+ models, 28 sets, two lobby scenes) EUR 199 (was EUR 298); formats max/fbx/obj/c4d/blend/usd; the scenes are Corona/V-Ray/Cycles only (not UE) — [Evermotion Hotel Lobby Furniture Bundle](https://evermotion.org/shop/show_product/hotel-lobby-furniture-bundle-vol-1/20839)
- Evermotion shows Archmodels for UE5 and Archinteriors for UE volumes, including office spaces (vol. 11), art galleries (vol. 12) and hospital (vol. 14) — [Evermotion on ArtStation](https://evermotion.artstation.com/)

#### Inferences
- Evermotion furniture/props (600+ hotel lobby pieces for EUR 199) are likely the best value for luxury lobby set dressing if FBX/USD assets are converted into Nanite UE meshes; interiors scenes are showcase-oriented and need collision/gameplay passes.

#### Gaps
- UE 5.7/5.8 compatibility of Archinteriors scenes UNVERIFIED (tags stop at 5.4); game-use licence terms UNVERIFIED; collision/LOD UNVERIFIED.

### Dekogon Studios (dekogon.com) — specialist UE environment/prop studio

#### Takeaway
Credible studio for modular realistic props and environments; sells on Fab, Unity and CGTrader. Reputation signal only; not a one-stop hotel/street solution.

#### Cited Findings
- Dekogon sells prop packs, individual props and full environments on multiple storefronts (Unreal Marketplace/Fab, Unity Store, CGTrader) — [Dekogon shop](https://www.dekogon.com/shop)
- Its ArtStation environment album lists "The Matrix Awakens (UE5) – Freeway and City" and "Apartment", plus Palmetro City and Noir City Apartment (portfolio claim, not verified independently) — [Dekogon ArtStation environments](https://dekogon.artstation.com/albums/878178)
- Fab examples: Urban Background Buildings – VOL.1 $24.99; Future City VOL.1 – City Props (Nanite + LP) $28.99; Military Supplies VOL.1 Tents $29.99, VOL.7 Containers $34.99; Meridian 1988 modular environment $109.99 — [Fab search cards](https://www.fab.com/search?q=The%20Projects%20Modular%20City%20Pack%20Nanite), [Dekogon Fab seller](https://www.fab.com/sellers/Dekogon%20Studios), [military search](https://www.fab.com/search?q=military%20base%20realistic&channels=unreal-engine)

#### Inferences
- Good source for industrial/military props and background buildings that fit a stealth-infiltration setting.

#### Gaps
- Dekogon's Matrix Awakens involvement is a portfolio claim; no primary Epic credit read.

### Poliigon (poliigon.com) — subscription scanned materials/HDRIs

#### Takeaway
High quality scanned materials with UE5 documentation, but the terms (no bots/scripts, no API without consent, AI/ML prohibition, no games with user access to assets) conflict with AI-agent automation; prices unverified. Prefer Megascans/Poly Haven/ambientCG.

#### Cited Findings
- Terms and licensing as in section C; plans billed monthly/yearly — [Poliigon pricing](https://www.poliigon.com/pricing), [Poliigon Asset Use & Licensing](https://help.poliigon.com/en/articles/8749749-asset-use-licensing)
- UE5 import documentation (2026-02-19): textures with ORM packing, FBX models, HDRIs, UE 5.4+ recommended — [Poliigon: Using Poliigon Assets in UE5](https://help.poliigon.com/en/articles/10831513-using-poliigon-assets-in-unreal-engine-5-updated-2026)

#### Inferences
- Manual downloads are fine, but scripted agent downloads would violate the Unlimited plan terms.

#### Gaps
- Subscription prices not captured: UNVERIFIED.

### Unity Asset Store (cross-engine option)

#### Takeaway
Legally usable in UE if the asset's EULA allows (standard EULA does), but delivered as Unity packages (FBX/textures plus Unity materials), so it needs conversion; low priority.

#### Cited Findings
- Unity states assets are not restricted to Unity projects; follow the EULA — [Unity Support](https://support.unity.com/hc/en-us/articles/34387186019988-Can-I-use-assets-from-the-Asset-Store-with-other-engines)

#### Inferences
- Only worthwhile for a specific pack unavailable elsewhere.

#### Gaps
- Per-asset EULAs may restrict engines; each must be read.

## 2. Category verdicts

### Takeaway
Fab is the primary buying channel; KitBash3D perpetual Gameplay Ready kits are the best verified hero-architecture source; Megascans plus Poly Haven/ambientCG cover surfaces and props; no verified AAA luxury-hotel interior exists off the shelf, so plan a hybrid build.

### Cited Findings
- Supporting evidence is in sections A, B, C and 1 above (each verdict is a synthesis; see those sections for URLs).

### Inferences
- **Best overall:** Fab (UE-native packs, in-editor access, Standard License) combined with KitBash3D Gameplay Ready perpetual kits. Reason: native .uasset delivery, clear licence, widest selection.
- **Best free:** (1) Poly Haven and ambientCG (CC0, zero licensing risk); (2) Fab free-tier Megascans (1,500+ announced) and free Megaplants under Fab Standard; (3) Fab bi-weekly Limited-Time Free and Epic UE samples (use for tests); (4) Cargo's 100+ free assets (licence terms for those free items UNVERIFIED).
- **Best paid (by evidence of game-readiness):** KitBash3D Paris/Hong Kong/Neo SF at $245 each (Nanite/collision/modular claims are documented by the vendor); PropHaus City Streets and Beach City at $129.99 (modular exterior+interior, level-instance buildings); Evermotion Hotel Lobby furniture bundle (EUR 199) for luxury dressing; The Projects at $324.99 for a dense noir/retro city (style mismatch if contemporary look needed).
- **Best for indie (solo or tiny team):** Personal-tier Fab purchases (<$100k trailing revenue), one or two perpetual KitBash3D kits (avoid subscriptions that lapse), Megascans per-asset buys ($0.99-$4.99), plus CC0 libraries; keep everything in a private repository.
- **Premium shortlist for luxury hotel + city street espionage (advertised capabilities, verified prices where shown):**
  1. KitBash3D Paris — $245 — https://kitbash3d.com/products/paris — Haussmann blocks, luxury shops, metro/train stations; Gameplay READY.
  2. KitBash3D Neo San Francisco — $245 (also Fab From $249.99) — https://kitbash3d.com/products/neo-san-francisco — contains Grand Hotel, Hotel Cali, The Luxe, Luxury Apartments; interior-ready shells; futuristic styling.
  3. PropHaus City Streets – Modular Pack — From $129.99 (4.8 from 25) — https://www.fab.com/listings/8b29843a-a98d-4f86-9c3f-8ec9b511347a — 20 prebuilt level-instance buildings, Nanite+Lumen, exterior+interior pieces (UE range on current listing UNVERIFIED).
  4. PropHaus Beach City – Modular Pack — From $129.99 (4.8) — https://www.fab.com/listings/c90bc1f1-3d5d-469e-a1d3-df2b439753f0 — hotels/resorts with interiors, UE 5.3–5.7, Miami Art Deco; recommends MegaLights.
  5. Leartes Studios Modern City Downtown Megapack — From $99.99 (4.5 from 123) — https://www.fab.com/listings/e6bae9e3-10eb-4f9f-aa93-c09608e782f9 — 355 meshes, modular buildings with restaurant/bank/metro interiors, listed 4.19–5.7.
  6. OrcaDomain The Projects – Modular City Pack – Nanite — From $324.99 (4.2 from 5) — https://www.fab.com/listings/173d12c0-fead-4f89-aa6f-e85214406d29 — 1,500+ Nanite meshes, 25 buildings with interiors, World Partition demo; retro-futuristic 20th-century USA style.
  7. Tav Realistic City – San Francisco Environment Mega Pack (Deluxe) — card From $1,499.99 (5.0 from 3; price attribution to this listing inferred from matching rating count, Medium confidence) — https://www.fab.com/listings/bb18a4ef-fddd-44bc-9f9b-6c7661d623de — fully playable 1 km x 1 km area, simple interiors; UE4+UE5 (5.8 status UNVERIFIED).
  8. Evermotion Hotel Lobby Furniture Bundle vol. 1 — EUR 199 — https://evermotion.org/shop/show_product/hotel-lobby-furniture-bundle-vol-1/20839 — 600+ hotel/lounge props in FBX/OBJ/USD (not UE-native).
  9. Quixel Urban Megadrop (600+ assets) — individual assets $0.99-$2.99 — https://www.fab.com/search?q=Quixel%20Urban%20Megadrop — streets, curbs, facades, modular building pieces; free items present.
- **Not recommended:** pirate mirrors (assetsue.com, psdly.co.uk and similar, which re-host paid Fab packs) — license violation; Editorial-labelled TurboSquid/CGTrader/Sketchfab items — not game-licensed; ArtStation Standard licence (2,000-sales cap) — use Extended only; Poliigon for agent-driven workflows — bot/API/AI terms; Sketchfab NC/ND/SA models — incompatible with commercial use; Unity Asset Store packs as primary source — conversion overhead; cheap AI-generated or one-review hotel packs without a demo video — unverified quality; and any pack that bundles real-world brand logos without clearance.

### Gaps
- No product was opened or tested, so "best" means best documented, not best proven. A 1-2 day pilot of KitBash3D Paris and PropHaus City Streets in a 5.8 project is advised before larger purchases.

## 3. Tool database rows

Notes: Pricing is lowest tier, as seen in cached crawls (see header). "UNVERIFIED" = not confirmed from a primary source in this research.

| Name | Category | Developer | URL | Purpose | Quality | Pricing | License | UE compatibility | Formats | AI integration (API/MCP/scripting/none) | Advantages | Limitations | Hardware | Recommendation | Confidence | Sources (URL + access date) |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| Fab marketplace | Marketplace (all asset types) | Epic Games | https://www.fab.com/ | Primary storefront for UE assets, Megascans, KitBash3D | Varies widely by publisher | Free to very high; typical packs $20-$700, mega packs up to about $1,500 | Fab Standard License (Personal/Professional tiers same rights; CC-BY on some) | UE-native packs; listing shows seller-declared UE version range | .uasset projects, some FBX/GLB/3ds Max | In-UE Fab plugin; per-listing "Allows usage with AI" flag; public download API UNVERIFIED | One-stop, in-editor, clear licence | Quality inconsistent; small review counts; versions lag 5.8; Personal tier <$100k revenue | Per product | Essential | High | https://www.fab.com/eula ; https://www.fab.com/channels/unreal-engine (2026-10-09) |
| Quixel Megascans (on Fab) | Scanned surfaces/props | Quixel (Epic) | https://www.fab.com/sellers/Quixel%20Megascans | Photoreal scanned materials, meshes, decals | Very high (advertised standard topology, UVs, LODs, PBR) | $0.99-$4.99 typical per asset seen; packs from $24.99 (2024 policy); some Free | Fab Standard License | Via Fab in UE 5.3+; new Megaplants UE 5.8+ | fbx, glb, +2 (UE via Fab) | Fab in Launcher/UE integration; no agent API verified | Industry-standard scans; Urban Megadrop 600+ assets | No longer free overall; pack price UNVERIFIED; legacy Bridge closing | Heavy textures/mesh | Essential | High | https://quixel.com/news/discover-the-latest-quixel-megascans-and-free-megaplants ; https://forums.unrealengine.com/t/reminder-free-megascans-ends-soon/2203090 (2026-10-09) |
| Quixel Megaplants | Vegetation | Quixel (Epic) | https://quixel.com/news/discover-the-latest-quixel-megascans-and-free-megaplants | Photoreal trees for PVE/Nanite | High (advertised Nanite-compatible) | Free | Fab Standard License | New releases UE 5.8+ only; PVE | UE plugin-based assets | None verified | Free, photoreal | 5.8+ only for new; PVE features partially experimental | Nanite-capable GPU | Valuable | Medium | https://quixel.com/news/discover-the-latest-quixel-megascans-and-free-megaplants (2026-10-09) |
| Poly Haven | Free library (HDRI/textures/models) | Poly Haven | https://polyhaven.com/ | CC0 HDRIs, PBR textures, props | High, photoreal; small catalog (~2,370 assets) | Free (Patreon optional) | CC0 | Engine-agnostic; import manually | glTF, Blend, FBX, EXR, PNG | Public no-key API (api.polyhaven.com); ToS forbids scraping | Zero licensing risk | Only ~521 models; no environments | Low | Essential | High | https://polyhaven.com/license ; https://app.cinevva.com/guides/sketchfab-polyhaven-kenney (2026-10-09) |
| ambientCG | Free library (materials) | ambientCG | https://ambientcg.com/ | CC0 PBR materials, HDRIs, models | High for materials | Free (Patreon/Ko-fi optional) | CC0 | Engine-agnostic | PBR map sets, HDR, models | API UNVERIFIED | CC0 explicitly allows game use | Materials focus; counts UNVERIFIED | Low | Valuable | High | https://ambientcg.com/license (2026-10-09) |
| KitBash3D (Cargo + Kits) | Premium architecture kits | KitBash (KitBash3D/Greyscalegorilla) | https://kitbash3d.com/ | World/interior kits, hero props | High; Gameplay Ready UE variants | Kits $245 perpetual (Individual); subscription $119/mo or $708/yr; Teams $99/user/mo | Commercial incl. games; perpetual vs subscription; Individual = single person only | UE 5.2+ (Gameplay Ready) | USD/UE/FBX etc. via Cargo | Cargo one-click send; agents.md published; no public API verified | Documented Nanite, collision, PLI | Subscription lapse blocks new projects; solo-only Individual; 15 GB per kit | 6-core CPU, 8 GB VRAM, 32 GB RAM, 15 GB disk per kit | Essential | High | https://kitbash3d.com/pages/gameplay-ready-kits ; https://kitbash3d.com/pages/licenses ; https://kitbash3d.com/pages/pricing (2026-10-09) |
| KitBash3D Paris | Modern city kit | KitBash3D | https://kitbash3d.com/products/paris | Haussmann European city blocks, luxury shops, metro | High (vendor) | $245 | Perpetual; tiered by company size | UE 5.2+; Gameplay READY | UE, others via Cargo | Cargo | European espionage setting; collision/Nanite | Vendor claims untested; no furnished hotel | As above | Essential | Medium | https://kitbash3d.com/products/paris (2026-10-09) |
| KitBash3D Neo San Francisco | Modern/futuristic city kit with hotels | KitBash3D | https://kitbash3d.com/products/neo-san-francisco | High-rise hotels/towers | High (vendor) | $245 (Fab From $249.99) | Perpetual; tiered | Interior-ready via upgraded importer | UE, others | Cargo | Includes Grand Hotel, Hotel Cali, The Luxe | Futuristic tone; shells unfurnished | As above | Valuable | Medium | https://kitbash3d.com/products/neo-san-francisco ; https://help.kitbash3d.com/en/articles/2445183-understanding-interiors-in-kitbash3d-kits (2026-10-09) |
| PropHaus City Streets – Modular Pack | Modular city environment | PropHaus | https://www.fab.com/listings/8b29843a-a98d-4f86-9c3f-8ec9b511347a | Playable American street blocks with interiors | High (advertised AAA, Nanite+Lumen) | From $129.99 | Fab Standard | UE5 Nanite/Lumen; current range UNVERIFIED | .uasset | None | 1,000+ meshes, 20 prebuilt buildings, demo map | Interiors partial; US look; no characters/vehicles | Nanite/Lumen capable GPU | Valuable | Medium | https://forums.unrealengine.com/t/prophaus-city-streets-modular-pack/2426163 (2026-10-09) |
| PropHaus Beach City – Modular Pack | Modular resort/hotel city | PropHaus | https://www.fab.com/listings/c90bc1f1-3d5d-469e-a1d3-df2b439753f0 | Hotels, resorts, shops with interiors | High (advertised) | From $129.99 | Fab Standard | UE 5.3-5.7 | .uasset | None | Hotels/resorts/pool, MegaLights guidance | Not labelled 5.8; Miami styling | Nanite/Lumen GPU | Valuable | Medium | listing URL (2026-10-09) |
| Leartes Modern City Downtown Megapack | City environment | Leartes Studios | https://www.fab.com/listings/e6bae9e3-10eb-4f9f-aa93-c09608e782f9 | Downtown with restaurant/bank/metro interiors | Good (4.5 from 123) | From $99.99 | Fab Standard | 4.19-5.7 | .uasset | None | Large rating base, modular, vehicles | Not 5.8-labelled; generic quality | Standard | Valuable | Medium | listing URL; https://www.fab.com/search?q=Modern%20City%20Downtown%20Megapack (2026-10-09) |
| OrcaDomain The Projects – Modular City Pack | City environment (retro-futuristic) | OrcaDomain | https://www.fab.com/listings/173d12c0-fead-4f89-aa6f-e85214406d29 | Dense noir/retro city blocks | Advertised AAA; 4.2 from 5 | From $324.99 | Fab Standard | UNVERIFIED | .uasset | None; AI usage flag "No" | 1,500+ Nanite meshes, World Partition demo | Style mismatch; few reviews | Nanite GPU | Optional | Low | https://www.fab.com/search?q=The%20Projects%20Modular%20City%20Pack%20Nanite (2026-10-09) |
| Tav Realistic City – San Francisco Deluxe | Playable city mega pack | Tav | https://www.fab.com/listings/bb18a4ef-fddd-44bc-9f9b-6c7661d623de | 1 km x 1 km downtown, buildings, props | Advertised AAA; 5.0 from 3 | Card From $1,499.99 (attribution by rating count) | Fab Standard; AI usage "No" | UE4+UE5; 5.8 UNVERIFIED | .uasset | None | Playable map, HISM/ISM optimization | Very high price; simple interiors; few reviews | Large-world capable PC | Optional | Low | https://www.fab.com/search?q=Realistic%20City%20San%20Francisco%20Deluxe (2026-10-09) |
| PolySphere Downtown – City Pack | Procedural/modular downtown | PolySphere Studio | https://www.fab.com/listings/bf3d8286-25e2-4cb5-a085-79312ea0c144 | NYC-Midtown-style city with editor tools | Good (4.4 from 22) | From $219.99 | Fab Standard | 5.0-5.8 shown, last update June 2023 | .uasset, fbx, 3ds Max | None | Collision and LODs listed, 609 meshes | Stale update date; editor-only tools | Standard | Optional | Medium | https://www.fab.com/search?q=Neo%20Renaissance%20Building%20Quixel (2026-10-09) |
| Evermotion Archinteriors for UE / Hotel Lobby Furniture | Arch-viz interiors and props | Evermotion | https://evermotion.org/shop/cat/397/archinteriors | Photoreal interior scenes and 600+ hotel props | Very high visual fidelity (arch-viz) | EUR 25-50 scene; EUR 79-199 volume/bundle; hotel bundle EUR 199 | Standard (<$1M revenue) / Professional; game terms UNVERIFIED | Scenes listed 5.3-5.4; 5.8 UNVERIFIED | UE scenes; props max/fbx/obj/usd | None | Luxury lobby dressing, cheap | Showcase-oriented; not UE-native for hotel bundle | RTX 3060, 32 GB RAM recommended | Valuable | Medium | https://evermotion.org/shop/show_product/hotel-lobby-furniture-bundle-vol-1/20839 (2026-10-09) |
| Hotel Lobby and Bar – Modular Interior | Hotel interior | CrescentDialGames | https://www.fab.com/listings/fc4a4c2c-1874-486b-8bac-e8d31cf749a9?lang=en | Modular hotel lobby/bar | Unproven; 5.0 from 4 | From $19.99 | Fab Standard | UNVERIFIED | .uasset | None | Cheap, modular grid | Quality unproven | Standard | Optional | Low | https://www.fab.com/search?q=luxury%20hotel&channels=unreal-engine (2026-10-09) |
| Hotel Lobby Environment (Tennur) | Hotel interior | Tennur | https://www.fab.com/listings/00739a3c-1dba-4754-bb12-94f43eb78019?lang=en | 138-mesh hotel lobby environment | Advertised game-ready | From $69.99 | Fab Standard | UNVERIFIED | .uasset | None | Mesh count | Motel-grade look | Standard | Optional | Low | listing URL (2026-10-09) |
| Dekogon Studios | Publisher (props/environments) | Dekogon | https://www.dekogon.com/shop | Industrial, military, urban props | Good (portfolio claims) | $5.99-$110 range seen | Fab Standard (on Fab) | Varies | .uasset | None | Specialist studio | Not complete hotel/city solution | Standard | Valuable | Medium | https://www.fab.com/sellers/Dekogon%20Studios (2026-10-09) |
| Military base packs (Leartes, Hivemind, FreshCan) | Military/industrial environment | Various | https://www.fab.com/search?q=military%20base%20realistic&channels=unreal-engine | Infiltration targets | Mixed; 4.7-5.0 ratings, small counts | $79.99-$249.99 | Fab Standard | UNVERIFIED | .uasset | None | Affordable bases | Quality unproven | Standard | Optional | Low | same URL (2026-10-09) |
| Sketchfab | CC model library | KitBash (since 2026-08-10) | https://sketchfab.com/ | Individual scans/props | Variable | Free CC; store closed | CC BY/CC0/NC/ND/SA; Standard; Editorial | Engine-agnostic | glTF, FBX, OBJ | Download API (700k+ CC models) | Huge variety | Attribution; NC/ND/SA traps; Editorial banned | Low | Optional | Medium | https://sketchfab.com/blogs/community/kitbash-acquires-sketchfab-and-artstation/ (2026-10-09) |
| ArtStation Marketplace | Artist-sold assets | KitBash (since 2026-08-10) | https://www.artstation.com/marketplace | Specialty packs | Variable; AI-content concerns | Varies | Standard (2,000-sales cap) / Extended / custom | Varies | Varies | None | Unique artists | Standard cap; UE-native uncommon | Varies | Optional | Medium | https://help.artstation.com/en/articles/16155014-eula-end-user-license-agreements (2026-10-09) |
| TurboSquid | Single 3D models | Shutterstock/TurboSquid | https://www.turbosquid.com/ | Hero props | Variable; archviz heavy | UNVERIFIED (example $199 lobby) | Royalty Free (with game conditions); Editorial banned for games | Engine-agnostic | max/FBX/OBJ | None | Large catalog | Not UE-native; Editorial traps | Varies | Optional | Medium | https://www.turbosquid.com/help/en/articles/9937422-royalty-free-license (2026-10-09) |
| CGTrader | Single 3D models | CGTrader | https://www.cgtrader.com/ | Hero props, kits | Variable | UNVERIFIED | Royalty Free / No AI / Editorial / Custom | Engine-agnostic | FBX, GLB, max, OBJ | None | Game-ready kits sometimes | Seller variance; no UE-native | Varies | Optional | Medium | https://www.cgtrader.com/pages/terms-and-conditions (2026-10-09) |
| Poliigon | Scanned materials | Poliigon | https://www.poliigon.com/ | Materials/HDRIs/models | High | UNVERIFIED (subscription) | Commercial Individual/Business; AI/ML banned; bots banned | UE5.4+ docs | PBR maps, FBX, HDRI | API needs consent; bots prohibited | Quality materials | Conflicts with agent workflow | Low | Avoid for automation | Medium | https://www.poliigon.com/terms (2026-10-09) |
| Unity Asset Store | Cross-engine option | Unity | https://assetstore.unity.com/ | Occasional pack | Variable | Varies | Standard EULA allows other engines | Conversion needed | Unity packages | None | Large catalog | Format conversion; per-asset EULAs | Varies | Optional | Medium | https://support.unity.com/hc/en-us/articles/34387186019988-Can-I-use-assets-from-the-Asset-Store-with-other-engines (2026-10-09) |
| Pirate mirror sites (assetsue.com, psdly.co.uk) | Illegal redistribution | N/A | (domains only, do not visit) | N/A | N/A | N/A | Violates seller licences | N/A | N/A | N/A | None | Illegal redistribution; unsafe | N/A | Avoid | High | https://www.fab.com/eula (standalone redistribution prohibited); observed in Exa search results (2026-10-09) |

## 4. Unresolved questions

### Takeaway
Remaining uncertainty is mostly empirical (does the pack work in 5.8 and look/perform like the screenshots) plus a few licence/price details that could not be fetched.

### Cited Findings
- (Evidence limits are described in the Gaps below; no new facts here.)

### Inferences
- A short pilot (buy one KitBash3D kit and one PropHaus pack, open in a 5.8.3 project) would resolve most remaining quality questions at under $400.

### Gaps
- Fab Professional-tier prices and the full Fab EULA clauses (AI, redistribution of cooked assets in a Steam build) not read from the primary legal text.
- Live prices: all Fab prices are cached card values; re-verify and note Fab shows VAT-inclusive EUR in some regions.
- Pack-level pricing for Quixel Urban Megadrop, Neo Renaissance Building, Pakistan Street, Abandoned Factory UNVERIFIED.
- Which PropHaus/Leartes/PolySphere packs have been retested for UE 5.8 UNVERIFIED.
- Whether any luxury-hotel interior pack exists with AAA-level materials and gameplay collision (I found none verified); a KitBash3D hotel-interiors kit UNVERIFIED.
- Fab and ambientCG scripting/API availability for AI coding agents (download API, rate limits, ToS) UNVERIFIED; Fab's planned public download API (announced by Sketchfab for 2025) status UNVERIFIED.
- Quality of Sketchfab, TurboSquid and CGTrader listings in the hotel/casino/street categories not assessed (search pages blocked).
- Evermotion licence terms for games and UE 5.7/5.8 compatibility UNVERIFIED.
- Industrial/military pack quality UNVERIFIED; UE 5.8 PCG/City-Sample-based city workflows are covered by another specialist.
- Characters, clothing, vehicles, weapons, animation and gameplay frameworks are outside scope (overlap only noted for stealth templates).
