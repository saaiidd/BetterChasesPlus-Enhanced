# Realistic Character Creation Pipeline for a UE5 Photoreal Espionage Game (Specialist 2 of 12)

Research date: 2026-10-09. All URLs accessed 2026-10-09 unless noted.

How the research was done: `dev.epicgames.com` (Epic's docs) returned 403 through the research proxy, so I could not open Epic documentation pages directly. Facts attributed to Epic docs come from search-engine snippets of those pages and are marked "(docs via search snippet)". Pages on metahuman.com, unrealengine.com, reallusion.com, daz3d.com, faceform.com and 3dscanstore.com were read in full. "UNVERIFIED" means I could not confirm the claim from a primary source this session. The benchmark games (Hitman: WoA, 007 First Light, GTA V) are quality references only. As general background, which I did not re-verify this session, they run on proprietary engines (IOI Glacier, Rockstar RAGE), so their character pipelines cannot be reused directly.

Version status as of Oct 2026:
- UE 5.8 / MetaHuman 5.8 shipped in June 2026. The metahuman.com announcement is dated 2026-06-11 and 80.lv reports the launch on 2026-06-18. [Corrected in verification: Epic's forum release posts for UE 5.8 and MetaHuman 5.8 are dated 2026-06-17 ([UE 5.8 Released](https://forums.unrealengine.com/t/unreal-engine-5-8-released/2729274); [MetaHuman 5.8 Released](https://forums.unrealengine.com/t/metahuman-5-8-released/2729288)), and the metahuman.com news index lists the 5.8 post under 2026-06-18. The 2026-06-11 date appears only in that page's metadata, so treat 2026-06-17 as the release date.]
- Epic describes UE 5.8 as the final major UE5 release ahead of UE6 ([80.lv](https://80.lv/articles/populate-your-ue5-8-worlds-with-metahuman-crowds)). [Corrected in verification: Epic's own wording is "the last planned major Unreal Engine 5 release on our roadmap", and it reserved "the option to release a 5.9, if needed" ([State of Unreal 2026](https://www.unrealengine.com/news/state-of-unreal-2026-top-news-from-the-show); [UE 5.8 blog](https://www.unrealengine.com/news/unreal-engine-5-8-is-now-available)). "Final" is 80.lv's paraphrase.]
- UE 5.9 has reportedly been announced, but I found no preview or MetaHuman 5.9 details (UNVERIFIED; [PCGH report](https://www.pcgameshardware.de/Unreal-Engine-Software-239301/News/Version-5-9-angekuendigt-KI-Features-im-Fokus-1551788/)). [Verification: partially confirmed. PCGH (2026-08-21) and Korean press coverage of Epic Korea's Unreal Fest Seoul 2026 recap ([khgames](https://www.khgames.co.kr/news/articleView.html?idxno=307619)) say 5.9 is in development, focused on MCP, Developer Assistant and semantic search, with no release date. I found no Epic English-language announcement post. UE6 Early Access is targeted for late 2027 per Tim Sweeney ([Inven Global](https://www.invenglobal.com/articles/24941/tim-sweeney-in-the-unreal-engine-6-era-we-expect-great-things-from-korean-developers)).]

---

## 1. Findings per tool (evidence + URLs)

### Takeaway
MetaHuman (inside UE 5.6–5.8) is now the strongest and cheapest base for photoreal humans in UE5:
- It is free under the UE EULA and can be used in any engine since June 2025.
- It has a parametric body, auto-resizing Outfit Assets, a Python/Blueprint creation API (5.7), and Mesh to MetaHuman for full bodies of any topology (5.8).
- MetaHuman Collections (5.8, Experimental) is an official crowd path built on Mass.

Reallusion CC5 is a capable paid alternative or companion. It offers lighter game bases, ActorMIXER blending, Headshot 3 AI photo-to-head, and AccuFace webcam capture. Its content licence forbids in-game or runtime character generation without an Enterprise licence. Daz has per-item licence friction. Blender/MPFB is the only fully free CC0 human generator found.

### Cited Findings

#### MetaHuman (Epic Games): Creator, Animator, Mesh to MetaHuman, Collections, Devkit

**5.6 (2025-06-03): MetaHuman left Early Access.**
- MetaHuman Creator (MHC), MetaHuman Animator (MHA) and Mesh to MetaHuman became "part of a single application" inside UE. Export was replaced by an "assembly step", and users get "full access to the source code of the MetaHuman Creator tool."
- "You will still need an internet connection… cloud services… deliver autorigging and texture synthesis." ([metahuman.com 5.6 announcement](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release))
- New parametric body system "based on extensive real-world scanned data", with constrainable height, chest, waist, leg length "and many more parameters" ([same](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release)).
- New UE Outfit Asset: garments authored in DCC apps are combined into an Outfit Asset. Garments are "pre-author[ed]… for a range of body sizes and shapes" and "interpolated to create clothing that fits any body, automatically resizing". Outfits can be sold on Fab and third-party marketplaces ([same](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release)).
- The scan database was "significantly expanded", and textures now retain "blemishes" for more lifelike skin ([same](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release)).
- All MetaHumans made in 5.6 or later "conform to the same common standards… enabling you to reuse and share animation, clothing, and grooms between them" ([same](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release)).
- MetaHuman for Maya: edit the mesh and expressions beyond the database while staying MetaHuman-compatible, and export XGen grooms as Alembic. MetaHuman for Houdini: procedural grooms such as ponytails, buns and braids, exported via Alembic ([same](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release)).

**5.7 (shipped with UE 5.7 on 2025-11-12; announced 2025-12-15).**
- MHC now runs on Linux and macOS.
- Body conforming supports arbitrary poses, and UV-space vertex correspondence allows FBX round-tripping with external DCCs.
- "Automate and batch process nearly all editing and assembly operations… using Python or Blueprints… The API supports sculpting, conforming, wardrobe, rigging, and textures". Scripts can run interactively or "on a compute farm".
- Houdini guide-driven parametric grooms. MetaHuman for Maya supports Maya 2023–2026. Joint-based groom animation is available, with Experimental blending of keyed animation and hair physics.
- Source: [metahuman.com 5.7](https://www.metahuman.com/releases/metahuman-5-7-is-now-available).

**Body conforming update (2026-03-19).**
- Conforming works for any pose between A-pose and T-pose. FBX round-trip via UV preserves vertex IDs, so you can "sculpt… in… Blender, ZBrush".
- Volumetric "Estimate Joints from Mesh" handles extreme proportions, and height range now extends beyond database limits.
- "Import meshes onto existing MetaHuman assets… create multiple variations of a character that share the same skeleton", and skeletons can be imported onto existing MetaHumans.
- Source: [metahuman.com](https://www.metahuman.com/news/metahuman-5-7-brings-major-improvements-to-body-conforming-with-more-to-come).

**5.8 (2026-06-11 announcement).**
- MetaHuman Collections (Experimental) populates worlds "scaling up to hundreds of characters on mobile and thousands on higher-end platforms".
- Characters transition between full Actors and Instanced Skinned Meshes (ISKMs) by camera distance. Crowds are built from modular head, body, hair and clothing parts, "by hand or procedurally using Blueprints".
- The workflow "relies upon Mass", can use Nanite, and animates via AnimBP or simple sequences depending on distance.
- New scalable face normal maps use "a magnitude less memory". A MetaHuman Crowds Sample is provided.
- Source: [metahuman.com 5.8](https://www.metahuman.com/news/metahuman-5-8-is-now-available).
- Mesh to MetaHuman now conforms bodies as well as heads, "accepts input meshes of any topology", and produces "a fully-rigged MetaHuman". Epic names "meshes generated with tools like Meshy or Tripo" as valid inputs. It is "constrained to humanoids" ([same](https://www.metahuman.com/news/metahuman-5-8-is-now-available)).
- Other 5.8 additions ([same](https://www.metahuman.com/news/metahuman-5-8-is-now-available)):
  - Unbaked texture and material export, with overrides applied back in MHC.
  - Custom lighting scenes in MHC, with Lumen support.
  - MHA available on Linux and macOS.
  - The audio-driven model adds procedural blinks and automatic emotion detection.
- OpenRigLogic: the RigLogic and DNA libraries are open source under MIT, as "native C++ libraries with Python bindings" ([MetaHuman Devkit page](https://www.metahuman.com/devkit)).
- The MetaHuman Creator web application is being discontinued (2026-03-05). New users have not been accepted since UE 5.6 ([metahuman.com news](https://www.metahuman.com/news); [creator-download page](https://www.metahuman.com/creator-download)).
- Epic's September 2026 learning roundup mentions "UE 5.8's MCP server" and an "Experimental AnimGen workflow". These belong to the MCP and animation specialists' scope ([metahuman.com news](https://www.metahuman.com/news)).
- Shipped or announced games using MetaHuman, per Epic:
  - Clair Obscur: Expedition 33 and The Sinking City 2 ([metahuman.com 5.7](https://www.metahuman.com/releases/metahuman-5-7-is-now-available)).
  - Mafia: The Old Country (Hangar 13, UE5), which used "Lumen, Niagara, MetaHuman" ([Unreal Engine developer interview](https://www.unrealengine.com/en-US/developer-interviews/mafia-the-old-country-making-the-old-feel-new-with-unreal-engine-5)).

#### Reallusion Character Creator 5 (CC5), ActorMIXER, Headshot 3

**Pricing:**
- CC5 Standard costs $299.
- CC5 Deluxe costs $479 and adds ActorMIXER PRO, the ActorMIXER CORE Library, HD Ultimate Morphs and the HD Human Anatomy Set.
- Source: [reallusion.com/character-creator](https://www.reallusion.com/character-creator/).

**Character bases (CC5 vs CC4 table on [reallusion.com/character-creator](https://www.reallusion.com/character-creator/)):**

| Base | Polycount | Notes |
| --- | --- | --- |
| HD base | 70K (SubD 1) | 2K/4K/8K textures |
| CC3+ base | 20K | |
| Game Base | 10K | Reduced bone count |
| ActorBUILD | 15K | Single material, for crowds |

**Features (same page):**
- The HD Facial Profile is listed as "MetaHuman Compatible", and CC5 can import MHA facial animation.
- InstaLOD LOD generation.
- Auto Setup plugins for Unreal, Unity, Blender, Maya and Marmoset.
- Marvelous Designer and GoZ links.
- Daz Transformer with HD morphs.
- Headshot and SkinGen Premium are extra-cost add-ons.

**CGChannel coverage ([CGChannel, updated 2025-07-21](https://www.cgchannel.com/2025/07/reallusion-unveils-character-creator-5/)):**
- Released 2025-08-27.
- HD bases have "two more subdivision levels" ("up to 16 times more mesh detail").
- The skin shader supports displacement and cavity maps.
- A Constraint System keeps expressions anatomically plausible.
- ActorMixer blends up to six characters. ActorMixer Pro ($199) adds one-click randomization and a CORE library of 40+ scanned heads.
- "Skeleton export geared for UE4 and UE5, with bind pose presets optimized for MetaHuman and UEFN."
- The CC UE Control Rig plugin was updated for CC5.
- The prior CC4.54 release was Windows-only; I found no macOS or Linux CC5 build (UNVERIFIED). [Verification: CGChannel's July 2026 Headshot 3.1 article lists Character Creator as "available for 64-bit Windows 7+" with a $299 perpetual licence or subscriptions from $29/month or $99/year ([CGChannel](https://www.cgchannel.com/?p=175693)). Windows-only is likely but still secondary-sourced.]

**Headshot 3 (launched April 2026):**
- A proprietary AI image-to-3D head model "trained on a large set of high-resolution facial scans". It can also produce full-body digital doubles from photos.
- A separate paid AI Image Generator uses Google "Nano Banana Pro".
- Price is UNVERIFIED. [Corrected in verification: CGChannel (updated 2026-07-07 for Headshot 3.1) gives a $199 perpetual licence. The head-generation AI model runs locally at no extra cost; the AI Image Generator consumes online "DA Points" (about $0.30 per 4K image) ([CGChannel](https://www.cgchannel.com/?p=175693)).]
- Sources: [befores & afters, 2026-04-30](https://beforesandafters.com/2026/04/30/reallusion-redefines-digital-double-creation-with-the-launch-of-headshot-3-for-character-creator-5/); [CGChannel, 2026-04-28](https://www.cgchannel.com/?p=175693).

#### Reallusion AccuRIG and ActorCore
- AccuRIG is a free standalone auto-rigger that handles scan and sculpt poses and exports FBX/USD to UE and others ([reallusion.com/auto-rig/accurig](https://www.reallusion.com/auto-rig/accurig/)).
- Features missing from the free version but present in CC: bone and mesh reduction, facial rig, skin-weight editing and merged materials ([same comparison table](https://www.reallusion.com/auto-rig/accurig/)).
- ActorCore is a store of mocap motions and "3D characters & scanned people for crowd animations" ([actorcore.reallusion.com](https://actorcore.reallusion.com/)). Its motion library is the animation specialist's scope.

#### Reallusion iClone 8 + AccuFace 2
- AccuFace 2 (2026-07-31) costs $299 MSRP and needs iClone 8.74+.
- It is designed for USB webcams or laptop cameras, with a recommended minimum of 720p/30fps, and works from a face region as small as 120×120 px.
- GPU: NVIDIA, or AMD Radeon RX 5700 and above. OS: Windows 10+.
- A new model targets the CC5 HD facial profile ("up to 128 lip morphs").
- iClone costs $599 perpetual, or $59/month or $199/year; CGChannel says "iClone Personal is free".
- Source: [CGChannel](https://www.cgchannel.com/2026/07/reallusion-launches-accuface-2/).
- The iClone Unreal Live Link plugin is reportedly free for developers with ≤$100K annual revenue. The article is undated, so this is UNVERIFIED as current ([ProVideo Coalition](https://www.provideocoalition.com/iclone-unreal-live-link-plug-in-is-now-free-for-independent-creators/)). [Verification: Reallusion's own Indie License page still offers it free if your annual gross revenue from design work is under USD 100,000, you own the full version of iClone 8, one licence per user or organization, schools excluded ([reallusion.com indie page](https://www.reallusion.com/iclone/live-link/unreal-engine/indie.html)). The free iClone Personal edition does not appear to qualify.]

#### Daz Studio / Genesis 9
- An Interactive License "is required when using or distributing 3D content from the Daz 3D store, whether modified or in its original form, in video games."
- "The creator of each product chooses whether or not to offer an Interactive License… If no Interactive License options are offered… it is not an option for using in… game." Licences are normally non-refundable.
- Source: [daz3d.com/interactive-license-info](https://www.daz3d.com/interactive-license-info).
- Per-item interactive licence prices of about $10/$35/$50 are user-reported only (UNVERIFIED; [Daz forum](https://free.downloads-cache.daz3d.com/forums/discussion/719796/interactive-game-developer-licensing-pricing-distribution-model-questions)).
- The Daz to Unreal Bridge supports Genesis 9, with IK Rig retargeting and ML Deformer integration. The bridge page lists UE 5.1/5.2 support; current UE 5.7/5.8 support is UNVERIFIED ([daz3d.com/unreal-bridge](https://delorean.daz3d.com/unreal-bridge)). [Corrected in verification: the official daz3d GitHub releases reach UE 5.3 (2023.1.2), with UE 5.4 plugin binaries added in April 2024 and a 2024 alpha pre-release ([daz3d/DazToUnreal releases](https://github.com/daz3d/DazToUnreal/releases)). A community fork maintained by David Vodhanel publishes UE 5.7 builds (v5.7.0.656, 2026-03-29) ([fork release](https://github.com/David-Vodhanel/DazToUnreal/releases/tag/v5.7.0.656)). No UE 5.8 build was found.]

#### Blender / MPFB 2 (MakeHuman for Blender)
- Core assets (base mesh, targets, skins) are CC0 and allowed in closed-source commercial games. The code is GPL (MPFB) or AGPL (MakeHuman), which does not cover your output. Third-party assets have their own licences.
- Sources: [MPFB FAQ](https://static.makehumancommunity.org/mpfb/faq/use_in_closed_source.html); [CGChannel, March 2025](https://www.cgchannel.com/2025/03/check-out-open-source-blender-character-generation-plugin-mpfb-2).
- I found no shipped commercial photoreal game using MPFB characters. Realism out of the box is well below MetaHuman (inference, not tested).

#### ZBrush
- ZBrush was not researched in depth because general modeling software belongs to another specialist. Pricing and licence are UNVERIFIED.
- Its role here is sculpting unique heads and bodies, then returning them to MetaHuman via the 5.7 FBX/UV round-trip ([metahuman.com](https://www.metahuman.com/news/metahuman-5-7-brings-major-improvements-to-body-conforming-with-more-to-come)) or to CC5 via GoZ ([reallusion.com](https://www.reallusion.com/character-creator/)).

#### Scan libraries and wrapping
- **3D Scan Store (run by Ten24):**
  - Sells HD heads, animation-ready heads, retopologised heads and expression packs.
  - It offers 3 licence types. The "Business Commercial Project Licence" is the one "to select if you are buying a product that you plan to integrate into a commercial, for sale product". An R&D licence must be upgraded if the project becomes commercial.
  - Source: [3dscanstore.com](https://www.3dscanstore.com/studios-and-commercial-customer-information).
  - Its free scans are personal use only ([80.lv via search](https://80.lv/articles/3d-scan-store-ships-a-new-high-quality-free-head-model/?amp=1)).
  - It shipped a pack of 69 MetaHuman identities built from its scans ([80.lv](https://80.lv/articles/3d-scan-store-ships-69-brand-new-metahumans)).
- **Faceform Wrap (formerly R3DS):**
  - Indie licence (revenue under $100K/yr): $570 perpetual, node-locked.
  - Professional: $1,070 node-locked or $1,470 floating. Windows and Linux.
  - It is a node-graph tool for fitting a clean base mesh onto raw scans. Wrap 2025.11 adds nodes for retargeting blendshapes and facial animation.
  - Sources: [CGChannel, 2025-12-26](https://www.cgchannel.com/2025/12/faceform-releases-wrap-2025-11-and-wrap4d-2025-11/); [faceform.com/wraporiginal](https://faceform.com/wraporiginal/).
- Texturing.XYZ, TEN24 custom scanning, Triplegangers and similar vendors were not verified this session (UNVERIFIED).

#### Clothing: Marvelous Designer / CLO
- Marvelous Designer 2025.2 added MetaHuman support. It imports MetaHumans in .dna format, and garments "resize automatically with the character" back in MHC ([CGChannel, Nov 2025](https://www.cgchannel.com/2025/11/clo-virtual-fashion-releases-marvelous-designer-2025-2/)).
- The official MD-to-MetaHuman workflow exports USD garments and fits them in the MetaHuman editor's Outfit Clothing section.
- ChaosCloth compatibility: MD 2025.0 works only with UE 5.4, MD 2025.2 works with UE 5.6+, and UE 5.5 is not supported.
- Source: [Marvelous Designer support](https://support.marvelousdesigner.com/hc/en-us/articles/52699135975705-Marvelous-Designer-to-MetaHuman-USD-Garment-Integration-Workflow).
- [Verification: the compatibility list above is confirmed, and it applies only to ChaosCloth assets; regular meshes and materials are unaffected. MD 2025.2 is no longer current: MD 2026.0 shipped in April 2026 and MD 2026.1 on 2026-08-24. MD 2026.1 is rental-only (Windows 11, Rocky/RHEL Linux 9.5+, macOS 14+). Personal plans cost $39/month or $280/year, and Enterprise plans $199/month or $2,000/year ([CGChannel MD 2026.1](https://www.cgchannel.com/2026/08/clo-virtual-fashion-releases-marvelous-designer-2026-1/)). The support article does not yet list 2026.x versions, so ChaosCloth compatibility of 2026.x with UE 5.8 is UNVERIFIED.]
- Epic ran a webinar on 2026-04-24 on dressing MetaHumans with CLO and Marvelous Designer ([metahuman.com news](https://www.metahuman.com/news)).

### Inferences
- MetaHuman covers every tier (hero, cast, crowd) with one skeleton standard, one shader family and one capture pipeline, at no licence cost for a UE game. That makes it the default for a solo developer.
- Since 5.7, every MHC operation is scriptable from Python, so an AI coding agent (Claude Code) can generate and batch-assemble civilian variants headlessly. This is the strongest AI-automation hook in this category. [Corrected in verification: Epic says "nearly all editing and assembly operations" are scriptable, not every one, and scripts run "interactively in the Unreal Editor or on a compute farm" ([metahuman.com 5.7](https://www.metahuman.com/releases/metahuman-5-7-is-now-available)). Autorigging and texture synthesis still use Epic cloud services and need an internet connection (5.6 announcement), so fully offline headless runs are not guaranteed. EULA update 21 also "defines the use of cloud features"; whether bulk automated cloud requests are rate-limited is UNVERIFIED.]
- CC5 is most valuable for its 10K/15K-poly game and crowd bases and for Headshot 3. With MetaHuman Collections available, it is no longer necessary for a UE-only project.

### Gaps
- I could not read Epic's docs pages directly, so MetaHuman triangle and joint counts per LOD and the names of the assembly quality tiers were not confirmed.
- I found no independent benchmark of MetaHuman Collections (frame time per N characters on specific GPUs).
- ZBrush and Marvelous Designer pricing were not checked here; they belong to other specialists. [Verification: Marvelous Designer pricing was found during verification; see the Clothing section above.]

---

## 2. Facial realism, skin and eyes, hair (strands vs cards), proportions, clothing

### Takeaway
Out of the box, MetaHuman leads on facial realism and anatomical plausibility: a scan-database-driven face and body, RigLogic expressions, and a skin texture database improved in 5.6. Strand grooms are cinematic-grade but expensive, and Epic's LOD system swaps strands for hair cards at LOD1+. Clothing is now handled by the Outfit Asset with auto-resize, plus Marvelous Designer 2025.2 interop, which suits suits and tactical gear.

### Cited Findings
- The MHC database of "real-world scan data" was expanded in 5.6, and texture processing keeps "authentic realism, such as blemishes" ([metahuman.com 5.6](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release)).
- 5.7 presets reportedly include 152 HD facial textures and new hairstyles. This comes from 3D Art coverage, not Epic, so it is UNVERIFIED ([3dart.it](https://www.3dart.it/en/new-unreal-engine-5-7-with-built-in-metahuman-creator-plugin/)).
- 5.8 lets you export unbaked textures and materials for editing in UE or DCCs and apply them as overrides, keeping baked assemblies for performance ([metahuman.com 5.8](https://www.metahuman.com/news/metahuman-5-8-is-now-available)).
- In the LODSync component, "Setting Min LOD to 1 disables certain animation features, uses hair cards instead of a groom, and uses lower quality materials" (docs via search snippet: [LODSync docs](https://dev.epicgames.com/documentation/en-us/metahuman/lodsync-component-for-unreal-engine)).
- Groom LODs support three geometry types per LOD (strands, cards, meshes). The Hair Card Generator (Dataflow) builds cards and LODs from a groom (docs via search snippet: [groom LOD docs](https://dev.epicgames.com/documentation/unreal-engine/setting-up-level-of-detail-for-grooms-in-unreal-engine); [Hair Card Generator](https://dev.epicgames.com/documentation/unreal-engine/creating-hair-cards-and-lods-using-hair-card-generator)).
- Many third-party MetaHuman grooms on Fab are strand-only, with cards not included (search result on [forum listings](https://forums.unrealengine.com/t/khitrovart-advanced-male-pattern-baldness-short-senior-metahuman-groom/2741575)).
- UE 5.7 grooms can be art-directed with joints and blended with physics ([metahuman.com 5.7](https://www.metahuman.com/releases/metahuman-5-7-is-now-available)).
- The parametric body can constrain measurements (height, chest, waist, leg length…) ([metahuman.com 5.6](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release)). 5.7 controls make it "easier to stay within human proportions" ([metahuman.com 5.7](https://www.metahuman.com/releases/metahuman-5-7-is-now-available)).
- Outfit Asset: garments are pre-authored at several sizes and interpolated to fit any body ([metahuman.com 5.6](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release)). An official parametric "MetaHuman Techwear Outfit" (jacket, pants, shoes) resizes in MHC ([UE forum 5.6 release thread](https://forums.unrealengine.com/t/metahuman-5-6-release/2539165)).
- CC5 skin shader has displacement and cavity maps. HD bases go up to 70K polys at SubD1 with 8K textures. The SkinGen layer editor and Digital Human Shader are included ([reallusion.com](https://www.reallusion.com/character-creator/); [CGChannel](https://www.cgchannel.com/2025/07/reallusion-unveils-character-creator-5/)).

### Inferences
- **Hero hair:** use strands in cutscenes and close third-person camera, and force cards when not in focus.
- **Civilian hair:** use cards or mesh hair only, since strand data is reportedly not streamed (forum claim, UNVERIFIED).
- **Short and buzz-cut grooms** are cheap and suit guards. Helmets, caps and balaclavas in tactical outfits cut hair cost further, which fits an espionage game.
- **Suits and tactical gear:**
  1. Model the garment in Marvelous Designer 2025.2.
  2. Export to USD and build the MetaHuman Outfit Asset.
  3. Author hard-surface gear (plate carriers, holsters, radios) in Blender as rigid attachments skinned to the body.
  4. Rely on the MetaHuman shared skeleton so one outfit fits all body types via resizing.
  Whether MetaHuman resizing works on rigid plate gear is untested (UNVERIFIED).

### Gaps
- I found no official ms or VRAM figures for strand vs card hair per character.
- A widely repeated forum claim says 50 card-only MetaHumans use about 800 MB VRAM for grooms. It is anecdotal and I did not read the thread.
- A Medium article estimates 3–6 ms GPU per LOD0 character with strands (UNVERIFIED, low confidence; [Medium](https://medium.com/@Jamesroha/metahuman-5-6-5-7-pipeline-reference-170d302b078e)).

---

## 3. Rig/skeleton compatibility (UE5 Mannequin, Control Rig), expressions, blendshapes

### Takeaway
MetaHuman bodies share the UE5 Mannequin skeleton and Control Rig, so retargeting is trivial. Faces use RigLogic (joints plus correctives defined in DNA), which is now open-sourced under MIT. CC5 uses blendshape-based facial profiles, with an HD profile labelled MetaHuman-compatible for MHA data. CC5 exports UE4/UE5 skeletons with MetaHuman-optimized bind poses.

### Cited Findings
- Epic's Mannequins pack: "These Mannequins use the same skeleton and Control Rig as MetaHumans, meaning that you can easily retarget animations" (via search snippet: [Mannequins asset pack](https://docs.unrealengine.com/marketplace/en-US/product/mannequins-asset-pack)).
- A UE 5.7.2 user reported hand and IK differences in the retargeter preview, so direct playback without the IK Retargeter is not guaranteed ([UE forum](https://forums.unrealengine.com/t/retargeting-ue4-weapon-animations-to-metahumans-accurately/2764682)).
- Bone counts: a community optimization guide reports "342 body bones & 875 face bones". It recommends LOD bone settings, the Animation Budget Allocator, and notes that the Skeletal Merging plugin breaks morph targets on merged MetaHumans ([EDC community tutorial](https://dev.epicgames.com/community/learning/tutorials/2JVl/unreal-engine-metahuman-skeletal-mesh-optimization-guide)).
- Epic talk "Creating Performant MetaHuman Characters" (Unreal Fest 2025):
  - Pose drivers were baked into RigLogic in 5.6, cutting cost from about 1.44 ms per character on PS5 (8–12 ms on Switch) to about 0.4 ms (transcript reads ".4").
  - The face AnimBP was removed from the chain, and head-movement IK can be disabled.
  - Source: [YouTube, Unreal Engine](https://www.youtube.com/watch?v=tTgMafRAM7A).
  - [Verification: numbers confirmed against the auto-generated transcript. The talk is from Unreal Fest Bali 2025, presented by Epic's Dustin Davau. The same talk says a full cinematic MetaHuman is about 800 MB on disk versus about 60 MB for the optimized "High" assembly, and recommends starting at LOD 2 for high-end PS5 games. These are speaker figures, not published benchmarks.]
- The OpenRigLogic and DNA libraries (MIT, C++ with Python bindings) load a MetaHuman "with the same runtime rig evaluation as Unreal Engine" ([metahuman.com/devkit](https://www.metahuman.com/devkit)).
- The 5.7 conforming changes let you import meshes onto an existing MetaHuman so that variants "share the same skeleton… same animation across them with predictable results" ([metahuman.com](https://www.metahuman.com/news/metahuman-5-7-brings-major-improvements-to-body-conforming-with-more-to-come)).
- CC5 facial profiles are Standard, Extended, Extended-Plus (with correctives) and HD (with correctives). The HD profile is marked "MetaHuman Compatible", and MHA facial animation import is supported (also in iClone 8) ([reallusion.com](https://www.reallusion.com/character-creator/)).
- CC5 offers "skeleton export geared for UE4 and UE5, with bind pose presets optimized for MetaHuman and UEFN" and a CC UE Control Rig plugin ([CGChannel](https://www.cgchannel.com/2025/07/reallusion-unveils-character-creator-5/)).
- The Daz to Unreal Bridge provides Genesis 9 IK Rig and retargeting plus ML Deformer ([daz3d.com](https://delorean.daz3d.com/unreal-bridge)).

### Inferences
- The body/mocap specialist's animation libraries, if authored for Manny, will retarget to MetaHumans with minimal setup. Plan IK Retargeter assets anyway for weapon-hand alignment.
- Avoid the Skeletal Mesh Merge plugin on hero and cast MetaHumans because it loses facial morphs. It may be acceptable for distant crowd proxies.

### Gaps
- I could not confirm official joint counts or face LOD joint-reduction tables (Epic docs inaccessible).

---

## 4. Facial performance capture options and hardware

### Takeaway
For a solo developer, MetaHuman Animator with an iPhone 12+ (offline depth solve for cinematics) plus audio-driven animation (for barks and VO) is the best value. A webcam works for real-time mono. A stereo head-mounted camera (HMC) is the high end. Body capture from a single webcam is Experimental in 5.8. Reallusion's AccuFace 2 ($299) is the webcam option inside the CC/iClone world.

### Cited Findings
- MHA in 5.6 added real-time capture from "mono cameras—including most webcams and many smartphones", "any camera that works with Unreal Engine LiveLink, including certain Android smartphones", and real-time audio-only animation with emotion and head movement ([metahuman.com 5.6](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release)).
- Capture device requirements (docs via search snippet: [Epic capture device requirements](https://dev.epicgames.com/documentation/metahuman/metahuman-animator-capture-device-requirements)):
  - **iPhone:** 12 or newer, with 16 or 16 Pro recommended, plus the Live Link Face iOS app. [Corrected in verification: the 16/16 Pro recommendation applies to real-time animation. For offline (depth) processing, which is the hero-cinematic path, the docs as quoted on Epic's forum recommend the iPhone 12, and Epic staff explained that older models give higher-quality depth data ([UE forum](https://forums.unrealengine.com/t/best-iphone-version-for-live-link-face-app/2385100)).]
  - **iPad:** 8th generation or newer. (UNVERIFIED in verification; docs blocked.)
  - **Android:** Android 13+ with a Snapdragon 8 Gen 1 through 8 Elite Gen 5 or Tensor G3–G5 chipset; Snapdragon 8s chips are not supported. Android is real-time only, with no offline capture. [Disputed in verification: the Google Play listing (updated 2026-06-05) names Snapdragon 8 Gen 1, 8+ Gen 1, 8 Gen 2, 8 Gen 3 and 8 Elite, and confirms 8s is unsupported ([Google Play](https://play.google.com/store/apps/details?id=com.epicgames.facelink&hl=en_US)). In August 2025 Epic staff said Tensor G3/G4 Pixels were not supported ([UE forum](https://forums.unrealengine.com/t/live-link-on-android-devices/2631460)). I could not confirm Tensor or 8 Elite Gen 5 support or the Android 13 minimum (CGChannel reported Android 12+ at launch). On offline capture, metahuman.com/download still says "offline processing (iOS only)", but the same June 2026 Play update "adds take recording for animation processing in Unreal Engine". Treat Android offline support as in flux and test before buying.]
- PC recommended spec (docs via search snippet: [Epic hardware requirements](https://dev.epicgames.com/documentation/metahuman/metahuman-hardware-requirements-in-unreal-engine)):
  - 16 high-performance physical cores and 32 GB RAM.
  - RTX 3070, RX 6800 XT or M2 Ultra GPU with 8 GB VRAM.
  - DX12 on Windows.
  - The Markerless Motion Capture plugin is Windows-only.
- 5.7 changes ([metahuman.com 5.7](https://www.metahuman.com/releases/metahuman-5-7-is-now-available)):
  - External USB cameras now work on iPad and supported Android devices; iPhones do not support external cameras.
  - Stereo HMC checkerboard calibration was improved, and an Experimental Calibration Diagnostics plugin was added.
- 5.8 changes ([metahuman.com 5.8](https://www.metahuman.com/news/metahuman-5-8-is-now-available)):
  - MHA captures "face, body, or both at once, from a single off-actor camera… a webcam is enough" using Meshcapade's Markerless Motion Capture (Experimental, Fab plugin, Windows).
  - Live Link Face streams video to UE in real time.
  - Solving is improved across "camera angles, lens distortion, and infrared lighting".
- AccuFace 2: $299; iClone 8.74+; 720p30 webcam minimum; NVIDIA or AMD RX 5700+ GPU; Windows ([CGChannel](https://www.cgchannel.com/2026/07/reallusion-launches-accuface-2/)).

### Inferences
- Minimum viable kit is a used iPhone 12+ and a phone head mount (mount price not researched), plus the PC spec above. [Verification: a used iPhone 12 is also Epic's recommended device for offline depth capture, so it is the best-value choice for cinematics; a 16/16 Pro mainly helps real-time.]
- Use offline depth processing for hero cinematics, real-time mono for previs, and audio-driven animation for hundreds of NPC barks.
- Buying or renting a stereo HMC is only worth it if cinematics are a core selling point.

### Gaps
- Stereo HMC vendors and prices were not researched; they are the mocap specialist's scope.

---

## 5. Crowd variation, LOD and performance (Mass)

### Takeaway
UE 5.8's MetaHuman Collections (Experimental) is now the official path to thousands of MetaHuman civilians: modular parts, ISKMs at distance, Mass orchestration, scalable normal maps and Nanite. A stock MetaHuman is heavy: about 800 MB cooked per Epic's 2024 UEFN talk, multi-component, with strand grooms. Crowds must therefore use optimized assemblies, LODSync min-LOD, card hair, budgeted animation and ISKMs.

### Cited Findings
- Collections: "hundreds of characters on mobile and thousands on higher-end platforms"; Actor-to-ISKM transitions by proximity; Mass orchestration; Nanite where available; AnimBP up close and simple sequences at distance; scalable normal maps at "a magnitude less memory" ([metahuman.com 5.8](https://www.metahuman.com/news/metahuman-5-8-is-now-available)).
- The 5.8 release-notes summary calls the experimental MetaHuman Crowd plugin a "complete assembly pipeline" from "tens to thousands of characters" (via search snippet: [5.8 release notes](https://dev.epicgames.com/documentation/metahuman/metahuman-5-8-release-notes-in-unreal-engine)).
- UEFN MetaHuman talk (Unreal Fest 2024) ([YouTube, Unreal Engine](https://www.youtube.com/watch?v=ygfgO2_WNm4)):
  - "The average Unreal MetaHuman cook size comes to about 800 megabytes", with a raw MetaHuman about 2 GB.
  - UEFN tiers use 4, 3 and 2 LODs (high, medium, low), derived from UE LODs, with LOD0 face skipped.
  - [Verification: confirmed against the transcript. The same talk reports texture baking plus groom decimation bringing a high-quality UEFN MetaHuman to about 60 to 64 MB cooked. The 2025 Bali talk gives a matching 800 MB (full cinematic) versus about 60 MB (optimized High) figure.]
- Epic talk ([YouTube](https://www.youtube.com/watch?v=tTgMafRAM7A)):
  - Separate face, body, clothing and groom components add overhead.
  - Choose the MetaHuman type by context (single hero vs squad vs crowd).
- LODSync coordinates face, eyebrows, lashes, hair, beard, torso, legs and feet LODs. Min LOD 1 swaps to cards (docs via search snippet: [LODSync](https://dev.epicgames.com/documentation/en-us/metahuman/lodsync-component-for-unreal-engine)).
- Community guidance ([EDC tutorial](https://dev.epicgames.com/community/learning/tutorials/2JVl/unreal-engine-metahuman-skeletal-mesh-optimization-guide)):
  - Use the Animation Budget Allocator (`a.Budget.*`).
  - Disable corrective bones by LOD.
  - Use Anim-to-Texture (VAT) for large crowds.
- Reference demo: The Matrix Awakens (Dec 2021) reportedly had 35,000 MetaHuman pedestrians in a 15.79 km² city (secondary reports: [AOL/Variety](https://www.aol.com/news/matrix-awakens-epic-games-040021751.html), [Dark Horizons](https://www.darkhorizons.com/?p=151475)). That was a tech demo with heavily simplified distant agents, not a shipped game. [Verification: Epic's own blog says "sixteen kilometers square" and "thousands of MetaHuman agents" without giving 35,000 ([Epic blog](https://www.unrealengine.com/en-US/blog/introducing-the-matrix-awakens-an-unreal-engine-5-experience)). Epic's City Sample docs (search snippet) say only the closest crowd members are fully rigged MetaHumans and distant ones are vertex-animated static meshes. The 35,000 figure remains secondary-only.]
- CC5 alternatives for crowds: ActorBUILD (15K polys, single material, "for crowd rendering") and Game Base (10K, reduced bones) ([reallusion.com](https://www.reallusion.com/character-creator/)).

### Inferences
- **Recommended civilian budget:**
  - Near (0–10 m): a few full MetaHuman Actors on optimized LODs with cards.
  - Mid-range: Collections ISKMs.
  - Far: Mass with simple sequences or VAT.
  - Exact counts must come from profiling on target hardware (rendering specialist's scope).
- Collections is Experimental in 5.8. Pin the engine version and keep a fallback of pre-assembled optimized MetaHumans plus the Animation Budget Allocator.

### Gaps
- I found no published fps or ms numbers for the MetaHuman Crowds Sample on named GPUs.
- Triangle counts per optimized LOD tier are unconfirmed.

---

## 6. AI-assisted character creation: what genuinely produces rigged, production-usable humans

### Takeaway
The only AI-adjacent paths that output fully rigged, engine-standard photoreal humans are:
- Mesh to MetaHuman (5.8), which converts any humanoid mesh, including Meshy/Tripo outputs, into a rigged MetaHuman.
- Reallusion Headshot 3, which turns photos into a CC5 head or body that inherits CC's rig.

In both cases the rig comes from the host system, not the AI. The audio-driven and mono-video MHA solvers are production AI for animation. Standalone "AI rigged human" generators remain research-grade.

### Cited Findings
- Mesh to MetaHuman: "accepts input meshes of any topology", produces "a fully-rigged MetaHuman with MetaHuman topology", and is "ideal for… meshes generated with tools like Meshy or Tripo". It is constrained to humanoids. This is advertised by Epic; I found no independent production test ([metahuman.com 5.8](https://www.metahuman.com/news/metahuman-5-8-is-now-available)).
- Headshot 3: photo-to-3D AI trained on facial scans; feature presets added because the "AI may not fully detect [details] from front-facing images"; full-body doubles. Advertised; no independent test found ([befores & afters](https://beforesandafters.com/2026/04/30/reallusion-redefines-digital-double-creation-with-the-launch-of-headshot-3-for-character-creator-5/)).
- MHA audio-to-face runs in real time with emotion, head motion (5.6), procedural blinks and automatic emotion detection (5.8). These are Epic-demonstrated features ([5.6](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release), [5.8](https://www.metahuman.com/news/metahuman-5-8-is-now-available)).
- Research state: OmniFaceRig (arXiv, June 2026) auto-generates FACS rigs with up to 155 blendshapes plus inner mouth from static meshes, but results are on "screened" benchmark inputs and there is no engine or production validation ([arXiv 2606.08043](https://arxiv.org/pdf/2606.08043)).
- Scripting hooks for AI coding agents:
  - MHC Python/Blueprint API (5.7) covers sculpting, conforming, wardrobe, rigging and textures ([metahuman.com 5.7](https://www.metahuman.com/releases/metahuman-5-7-is-now-available)).
  - OpenRigLogic has Python bindings ([devkit](https://www.metahuman.com/devkit)).
  - UE 5.8 has an MCP server (mentioned in [metahuman.com news](https://www.metahuman.com/news); details are the MCP specialist's scope).
- Licence constraint on AI:
  - The UE EULA change log "adds a restriction on use of MetaHuman digital characters and animation curves with AI or similar technologies". It also adds "express language against use of the Licensed Technology as a training input or prompt-based input into Generative AI Programs" ([UE EULA change log](https://www.unrealengine.com/eula-change-log/unreal)). [Corrected in verification: these are two different amendments. The Generative AI input language arrived in EULA update 20, and the MetaHuman AI restriction in update 21.]
  - A secondary summary says the prompt-input ban applies "where the Generative AI Program trains on input data" (UNVERIFIED wording; [ConductAtlas](https://conductatlas.com/platform/unreal-engine/unreal-engine-eula/ai-training-input-prohibition/)). [Corrected in verification: the wording is confirmed in the EULA itself (Section 6(e) general restrictions, read via search snippet of [unrealengine.com/eula/unreal](https://www.unrealengine.com/eula/unreal) and quoted verbatim in [EpicGames/OpenRigLogic issue #1](https://github.com/EpicGames/OpenRigLogic/issues/1)). Your activities must not "result in using the Licensed Technology as a training input to any Generative AI Program or as prompt-based input where the Generative AI Program trains on input data". A separate bullet bars you to "use, or permit others to use, MetaHuman digital characters and animation curves (or any rendered output thereof if crafted to replicate the functionality of MetaHuman) to build or enhance any database or training or testing any artificial intelligence, machine learning, deep learning, neural networks, or similar technologies." Two points matter here. The first clause covers all UE "Licensed Technology" (engine code, examples, starter content), not just MetaHuman. The second clause covers "testing" as well as training.]
  - Reallusion bans AI training on its content or renders without an Enterprise licence ([Reallusion content license](https://www.reallusion.com/license/content.html)). [Corrected in verification: the Content EULA (updated 2025-08-01) is broader. Section 2.1.B(C)(e) forbids use of Content or Derivative Content "For machine learning, AI training, or AI-generated output", and Enterprise is negotiated case by case ([Reallusion Content EULA](https://www.reallusion.com/Content/EULA/EULA.htm)).]

### Inferences
- **Safe AI use:** Claude Code writing MHC Python scripts to batch-create and assemble characters. This does not feed MetaHuman content into a generative model.
- **Grey zone:** sending MetaHuman renders or screenshots to an LLM or image model, for example for visual QA. This may count as "prompt-based input" depending on whether the provider trains on inputs. Get legal clarity first; see Unresolved.
- AI mesh generators (Meshy/Tripo) feeding Mesh to MetaHuman are useful for ideation of body shapes. Realism comes from MetaHuman's database, so the AI mesh mainly sets proportions. Check the generator's output licence (AI 3D specialist's scope).

### Gaps
- I found no hands-on, independent evaluation of Mesh to MetaHuman body conversion quality from AI meshes, or of Headshot 3.

---

## 7. Commercial licensing (mandatory gate for a Steam release)

### Takeaway
- **MetaHuman in a UE game: PASS.** It is included in the UE licence and free under $1M revenue, after which standard UE terms apply.
- **Reallusion CC5: PASS with conditions.** The Standard licence now covers export and games with unlimited characters. In-game or runtime character generation, DLC sale of RL content, and AI training need an Enterprise licence.
- **Daz: CONDITIONAL.** Every store item needs its own Interactive License, which the creator may not offer.
- **MPFB: PASS** (CC0 core).
- **Scans: PASS** only with the vendor's commercial licence. Real-person likeness needs a release.

### Cited Findings
- **MetaHuman:**
  - "Included as part of your standard Unreal Engine license… free to use for anyone making under $1 million USD in revenue… MetaHumans can be used with any engine or creative software" ([metahuman.com/license](https://www.metahuman.com/en-US/license)).
  - Above $1M company revenue, UE seats are required ($1,850/yr per CGChannel, June 2025). Outside UE, MetaHumans are "non-engine products" with no 5% royalty ([CGChannel](https://www.cgchannel.com/2025/06/you-can-now-sell-metahumans-or-use-them-in-unity-or-godot/)). For a UE game, the normal UE royalty terms apply; the overall-licensing specialist covers the details. The UE EULA change log references the "$1,000,000 lifetime gross revenue royalty exclusion" ([change log](https://www.unrealengine.com/eula-change-log/unreal)).
  - Fab MetaHuman purchases are governed by the UE EULA, Fab EULA and Fab Distribution Agreement ([UE forum 5.6 release](https://forums.unrealengine.com/t/metahuman-5-6-release/2539165)).
  - **Conflict to flag:** the Epic Content License's "MetaHuman Content Addendum" still says "MetaHuman Content that you acquire may only be used and shared as UE-Only Content". That contradicts the any-engine wording but is irrelevant for a UE game ([unrealengine.com/eula/content](https://www.unrealengine.com/eula/content)).
  - Under the same addendum, you own copyright and publicity rights in scans you upload, and in rendered depictions. Epic keeps MHC and Rig Logic ([same](https://www.unrealengine.com/eula/content)).
  - The legacy MetaHuman Creator (web) EULA barred use "with a bot or other automated tool" and AI training ([unrealengine.com/eula/mhc](https://www.unrealengine.com/en-US/eula/mhc)). It governed the now-discontinued web app; 5.7 officially provides scripting.
- **Reallusion:**
  - The Content License Policy page announces an "Expanded Standard License": "Export content to any external software or game engine; Reuse purchased assets across unlimited characters and projects… For mass distribution in… game titles" ([reallusion.com/license/content.html](https://www.reallusion.com/license/content.html)).
  - The same page still contains older FAQ text requiring an "Extended License" for CC Components in games. **This is a conflict on the same page, likely stale.**
  - Content EULA 2.1(B) under the Standard License: "use… in commercial games, AR/VR projects and interactive services… repeatedly use CC components to create unlimited characters across multiple titles" ([Reallusion Content EULA](https://www.reallusion.com/Content/EULA/EULA.htm)).
  - Content EULA 2.1(C) prohibits use "a. As purchasable in-software content or downloadable paid content. b. For online or in-software character generation. c. In character generation APIs" ([same](https://www.reallusion.com/Content/EULA/EULA.htm)).
  - The Enterprise licence covers "AI training / machine learning", "integrated character creation" and content integration ([reallusion.com/license/enterprise.html](https://www.reallusion.com/license/enterprise.html)).
  - A Reallusion staff forum answer: an in-game system "that will generate or randomise the looks of the characters" counts as character generation and needs Enterprise. Changing clothing per level is fine ([forum, Peter (RL)](https://forum.reallusion.com/PrintTopic526096.aspx)).
  - Asset Pack EULA §5, as quoted on the forum: CC-generated topology is Reallusion's property. You may not convert it to other formats for distribution or sale on third-party marketplaces, or use morphs to build a character generation system ([forum quote](https://forum.reallusion.com/541600/Creating-model-for-client-question)). This is a forum quote, so primary AP EULA text is UNVERIFIED.
  - If a Content One-Year Plan lapses, files saved in Reallusion's format revert to DRM trial content. Output works such as games keep their rights ([Content EULA §3](https://www.reallusion.com/Content/EULA/EULA.htm)).
- **Daz:** Interactive License per item; items with no IL option cannot go in games ([daz3d.com](https://www.daz3d.com/interactive-license-info)).
- **MPFB/MakeHuman:** CC0 core assets, no attribution needed ([MPFB FAQ](https://static.makehumancommunity.org/mpfb/faq/use_in_closed_source.html)).
- **3D Scan Store:** "Business Commercial Project Licence" for commercial for-sale products ([3dscanstore.com](https://www.3dscanstore.com/studios-and-commercial-customer-information)).

### Inferences
- For a UE-only Steam game, MetaHuman has the lowest licence risk of anything reviewed.
- Reallusion-based civilians are safe if they are pre-built, fixed characters. A runtime randomizer that mixes RL heads, hair and clothes probably triggers the "character generation" ban, and that is how one would naturally build a varied crowd.
- Never use real-person likeness (scans, Headshot photos, AI photos of celebrities) without a signed model or likeness release. The MetaHuman addendum makes you warrant rights to uploaded scans ([Epic content license](https://www.unrealengine.com/eula/content); [MHC EULA UGC clause](https://www.unrealengine.com/en-US/eula/mhc)).

### Gaps
- I did not retrieve the exact current UE EULA MetaHuman AI clause text; I only have the change-log summary.
- I found no dated changelog for Reallusion's "Expanded Standard License".

---

## 8. Recommended pipeline (protagonist / supporting cast / guards / civilians)

### Takeaway
Use MetaHuman-first on UE 5.8, or 5.7 if Experimental Collections is a risk you do not want. Bring uniqueness in from outside: a licensed scan or your own sculpt, custom textures, custom grooms and custom Marvelous Designer outfits. Then automate variant production with the MHC Python API driven by Claude Code. Reallusion is optional: AccuRIG (free) for odd meshes; CC5 and AccuFace only if you prefer that ecosystem.

### Cited Findings (basis for steps)
- Mesh to MetaHuman for heads and bodies from any topology ([5.8](https://www.metahuman.com/news/metahuman-5-8-is-now-available)).
- FBX/UV round-trip, import mesh onto an existing MetaHuman, and shared skeleton ([Mar 2026](https://www.metahuman.com/news/metahuman-5-7-brings-major-improvements-to-body-conforming-with-more-to-come)).
- Python API for batch editing and assembly ([5.7](https://www.metahuman.com/releases/metahuman-5-7-is-now-available)).
- Unbaked texture overrides ([5.8](https://www.metahuman.com/news/metahuman-5-8-is-now-available)).
- Outfit Asset resizing ([5.6](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release)) with MD 2025.2 USD ([MD support](https://support.marvelousdesigner.com/hc/en-us/articles/52699135975705-Marvelous-Designer-to-MetaHuman-USD-Garment-Integration-Workflow)).
- Collections plus Mass ([5.8](https://www.metahuman.com/news/metahuman-5-8-is-now-available)).
- Houdini and Maya groom tools ([5.7](https://www.metahuman.com/releases/metahuman-5-7-is-now-available)).
- LODSync and Hair Card Generator (docs via search snippets above).

### Inferences: exact steps and expected manual work
Time estimates are my judgement, not sourced.

**A. Protagonist** (1 character; about 3–6 weeks for one skilled artist, much longer for a beginner)
1. **Likeness source:** pick one.
   - (a) Buy a head scan under a Business Commercial Project Licence (3D Scan Store or similar).
   - (b) Photo-scan a consenting actor with a signed likeness release. Photogrammetry tools are another specialist's scope.
   - (c) Sculpt an original head in ZBrush or Blender.
2. **Clean-up:** wrap the scan to a clean topology in Faceform Wrap ($570 Indie), or feed it straight to Mesh to MetaHuman (accepts any topology).
3. **Mesh to MetaHuman in MHC:** fit head and body. Set proportions with the parametric body; a fit operative build helps distinguish the hero.
4. **Refine likeness:** use MHC sculpt, or the FBX round-trip to ZBrush/Blender via UV correspondence. Use MetaHuman for Maya for expression art-direction if available.
5. **Custom skin:** export unbaked textures (5.8), then paint unique details (scars, moles, pores and tattoos from your own sources) and re-apply as overrides.
6. **Custom groom:** author in Blender, Houdini (MetaHuman for Houdini) or Maya XGen, then export Alembic. Generate cards with the Hair Card Generator for gameplay LODs and keep strands for cinematics.
7. **Wardrobe:** author suits and tactical outfits in Marvelous Designer 2025.2, export USD, and build an Outfit Asset. Model hard-surface gear in Blender.
8. **Face animation:** offline MHA capture with iPhone 12+ (16 recommended) for cutscenes; audio-driven MHA for barks.
9. **Body animation:** retarget Manny-based mocap and libraries (animation specialist's scope).
10. **Runtime setup:** during gameplay use LODSync Min LOD ≥1 when not in close-up, card hair, and the Animation Budget Allocator.

**B. Supporting cast** (10–30 characters; about 1–4 days each once the pipeline exists)
1. Start each character from a different scan identity or preset blend. Avoid unmodified presets.
2. Blend at least 3 presets, then sculpt, and set a parametric body per character.
3. Give each one a unique texture override and an outfit variant.
4. Have Claude Code write MHC Python scripts that apply parameter sets from a JSON character bible, assemble the characters, and regenerate them after changes.
5. Use MHA (iPhone or webcam) for dialogue scenes.

**C. Guards** (archetypes ×N variants)
1. Use one tactical Outfit Asset family (fatigues, plate carrier, helmet or cap, balaclava) that resizes across body types.
2. Create 10–20 face/body variants via Python. Concealing gear lets faces be lower LOD and hair cheap.
3. Give each faction a material swap (camo, insignia).
4. Share the skeleton so all guards use one AnimBP.

**D. Civilians** (hundreds to thousands on screen)
1. Generate a pool of modular parts (heads, bodies, hair as cards only, clothing) with MHC Python: 30–60 heads × 6–10 bodies × 10–20 outfits × 15 hairstyles.
2. Build MetaHuman Collections and compose them procedurally in Blueprints.
3. Use Mass for behaviour, ISKMs at distance, scalable normal maps, and Nanite where supported.
4. Profile, then cap close-range full Actors.
5. **Fallback if Collections stays Experimental or unstable:** pre-assembled optimized MetaHumans, Animation Budget Allocator, and VAT crowds at range.
6. **Avoid as the primary crowd source:** a Reallusion runtime randomizer (licence), and marketplace "ready" MetaHumans used unmodified (they look generic).

**Steps that make characters look original**
1. Never ship a preset or Fab character unmodified.
2. Seed faces from licensed scans or original sculpts, not from the preset gallery alone.
3. Paint custom skin detail.
4. Author custom grooms and outfits; bought Fab outfits are acceptable only for civilians, recoloured.
5. Vary body proportions beyond default sliders.
6. Give each character distinct performance capture (gait and facial mannerisms).
7. Keep a "look bible" so AI-scripted variation stays art-directed.

---

## 9. Category verdicts

### Takeaway
MetaHuman wins overall, as the best free option, and for indie use. CC5 is the best paid companion. MHA plus Mesh to MetaHuman is the best AI-assisted path. Daz is not recommended for this project.

### Cited Findings
See Q1–Q7 citations. Verdicts are inferences built on them.

### Inferences
- **Best overall: MetaHuman (UE 5.8).** It covers scan-based realism, RigLogic faces, parametric bodies, Outfit Assets, the Python API, Collections crowds and integrated capture, all free for a UE game ([5.6](https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release), [5.7](https://www.metahuman.com/releases/metahuman-5-7-is-now-available), [5.8](https://www.metahuman.com/news/metahuman-5-8-is-now-available)).
- **Best free:**
  - MetaHuman (free under the UE licence).
  - For non-MetaHuman needs, MPFB 2 in Blender (CC0) and Reallusion AccuRIG (free auto-rig).
- **Best paid:**
  - Reallusion CC5 Deluxe ($479): fast authoring, game and crowd bases, ActorMIXER, and an MHA-compatible HD face.
  - Faceform Wrap Indie ($570) for scan processing.
  - Marvelous Designer 2025.2 for MetaHuman clothing; price not verified here.
- **Best AI-assisted:**
  - MetaHuman Animator (mono, webcam, audio-driven, plus Experimental markerless body).
  - Mesh to MetaHuman, which turns AI or scan meshes into rigged MetaHumans.
  - Runner-up: Reallusion Headshot 3 for photo-to-CC5 heads.
  - Automation: the MHC Python API with Claude Code.
- **Best for an indie developer:** MetaHuman + Marvelous Designer + an iPhone 12+/16 for MHA + Blender. Add Wrap only if you use scans.
- **Not recommended:**
  - **Daz Studio/Genesis for this game:** per-item Interactive Licenses that creators may not offer, and an Unreal bridge last confirmed for UE 5.1/5.2.
  - **MetaHuman web app:** discontinued.
  - **Reallusion content in any runtime randomizer or character creator:** needs an Enterprise licence.
  - **Skeletal Mesh Merge on facial MetaHumans:** loses morphs.
  - **Strand hair on crowds.**
  - **"One-click AI rigged human" generators as final assets:** only research-grade evidence (OmniFaceRig), no production validation.

---

## 10. Tool database rows

Notes on columns:
- Pricing is in USD.
- Sources were accessed 2026-10-09.
- "AI integration" means programmatic hooks an AI coding agent can drive.

| Name | Category | Developer | URL | Purpose | Quality | Pricing | License | UE compatibility | Formats | AI integration (API/MCP/scripting/none) | Advantages | Limitations | Hardware | Recommendation | Confidence | Sources (URL + access date) |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| MetaHuman Creator (in-engine) | Digital human creator | Epic Games | https://www.metahuman.com/create | Author faces and bodies, assemble characters | Top-tier photoreal (AAA-proven: Mafia TOC, Expedition 33) | Free (UE licence; standard UE terms above $1M revenue) | UE EULA; any-engine use allowed since 5.6; AI-training/prompt-input restriction | Native UE 5.6–5.8 (Win/Linux/macOS since 5.7) | UE assets, DNA, FBX round-trip (5.7), Alembic grooms, USD outfits (via MD) | Python + Blueprint API (5.7), UE 5.8 MCP server (UNVERIFIED scope) | Scan database, parametric body, shared standard, source code access | Needs internet (cloud autorig and texture synthesis); heavy at LOD0 | 16 cores, 32 GB RAM, RTX 3070 / 8 GB VRAM recommended | Essential | High | metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release; metahuman.com/releases/metahuman-5-7-is-now-available (2026-10-09) |
| Mesh to MetaHuman (head + body, 5.8) | Scan/mesh conversion | Epic Games | https://www.metahuman.com/news/metahuman-5-8-is-now-available | Convert any-topology humanoid mesh to rigged MetaHuman | High (advertised; no independent test found) | Free | UE EULA | UE 5.8 (bodies); heads earlier | Any mesh in, MetaHuman out | Python (MHC API) | Accepts scans, sculpts, Meshy/Tripo meshes | Humanoid only; extreme shapes "results may vary" | As above | Essential | Medium | metahuman.com/news/metahuman-5-8-is-now-available (2026-10-09) |
| MetaHuman Animator | Facial (and body) performance capture | Epic Games | https://www.metahuman.com/animate | Video, depth or audio to facial animation; single-camera body (Experimental) | High (iPhone depth/HMC); good (mono/webcam) | Free | UE EULA | UE 5.6–5.8; Linux/macOS since 5.8 | Live Link, capture data, anim sequences | Audio-driven AI solve; Python scripting UNVERIFIED | Cheapest high-quality face capture; real-time and offline | Android real-time only; markerless body Experimental and Windows-only | iPhone 12+ (16 recommended), iPad 8th gen+, Android 13+ Snapdragon 8 Gen 1+ / Tensor G3+, webcam, stereo HMC; PC as above | Essential | High | metahuman.com 5.6 and 5.8 news; dev.epicgames.com capture device requirements (search snippet) (2026-10-09) |
| MetaHuman Collections / Crowd (5.8) | Crowd system | Epic Games | https://www.metahuman.com/news/metahuman-5-8-is-now-available | Thousands of modular MetaHumans with Mass, ISKMs, Nanite | Good (scalable normal maps, near-cinematic close up, per Epic) | Free | UE EULA | UE 5.8 only (Experimental) | UE assets | Blueprint procedural composition; Python UNVERIFIED | Official crowd path; Actor↔ISKM LOD switching | Experimental; no public perf benchmarks found | High-end PC/console for thousands | Valuable (Essential once stable) | Medium | metahuman.com 5.8; 80.lv/articles/populate-your-ue5-8-worlds-with-metahuman-crowds (2026-10-09) |
| MetaHuman for Maya | Rig/mesh editing plugin | Epic Games (3Lateral) | https://www.metahuman.com/news/metahuman-leaves-early-access-with-a-feature-packed-new-release | Edit DNA meshes and expressions; XGen grooms | Pro-grade | Free plugin (Fab); Maya licence extra (UNVERIFIED price) | UE EULA / Fab | Round-trips DNA to UE | DNA, Alembic | Maya Python (UNVERIFIED specifics) | Expression art-direction beyond database | Requires Maya 2023–2026 | Workstation | Optional | Medium | metahuman.com 5.6 and 5.7 (2026-10-09) |
| MetaHuman for Houdini | Groom tool | Epic Games | https://www.metahuman.com/releases/metahuman-5-7-is-now-available | Procedural grooms (braids, buns, ponytails) | High | Free plugin (Fab); Houdini licence UNVERIFIED | UE EULA / Fab | UE via Alembic | Alembic | Houdini Python/HDA (UNVERIFIED) | Guide-driven parametric hair | Houdini learning curve | Workstation | Valuable (hero hair) | Medium | metahuman.com 5.7 (2026-10-09) |
| OpenRigLogic / MetaHuman Devkit | Rig runtime library | Epic Games | https://www.metahuman.com/devkit | RigLogic + DNA evaluation outside UE | n/a | Free | MIT | Matches UE evaluation | DNA | C++ and Python bindings | Tool-building and validation scripts | Early "start" of devkit | Any | Optional | High | metahuman.com/devkit (2026-10-09) |
| Live Link Face (iOS/Android) | Capture app | Epic Games | https://www.metahuman.com/animate | Stream or record face capture | High (iOS depth) | Free | Epic terms (UNVERIFIED) | UE Live Link | Live Link, video | n/a | Video streaming to UE (5.8) | Android: real-time only | iPhone 12+ / supported Android | Essential | High | metahuman.com 5.7 and 5.8; Epic capture docs (search snippet) (2026-10-09) |
| Reallusion Character Creator 5 | Character creator | Reallusion | https://www.reallusion.com/character-creator/ | Author realistic or stylized characters; game and crowd bases | High (HD base 70K, 8K textures); MetaHuman still leads on faces (inference) | $299 Std / $479 Deluxe (perpetual) | Reallusion EULA + content licence (Standard covers games; no runtime character generation without Enterprise) | Auto Setup for Unreal, UE4/UE5 skeleton export, UE Control Rig plugin | FBX, OBJ, USD, iAvatar | CC Python API UNVERIFIED this session | Game Base 10K, ActorBUILD 15K crowd base, InstaLOD LODs, MD/GoZ links, MHA import | Windows-only (UNVERIFIED for CC5); content licence complexity | Windows PC, GPU | Optional (Valuable if not using MetaHuman) | High | reallusion.com/character-creator; cgchannel.com/2025/07/reallusion-unveils-character-creator-5 (2026-10-09) |
| ActorMIXER PRO | Character blending | Reallusion | https://www.reallusion.com/character-creator/ | Blend up to 6 characters; randomize; 40+ scanned heads | Good | $199 (or in CC5 Deluxe) | Reallusion EULA | Via CC5 export | CC formats | One-click randomization (in-app) | Fast variant generation | Randomization only safe for pre-baked exports (licence) | Windows | Optional | Medium | cgchannel.com/2025/07/reallusion-unveils-character-creator-5 (2026-10-09) |
| Headshot 3 | AI photo-to-3D head/body | Reallusion | https://www.reallusion.com/character-creator/ | Photo(s) to CC5 head or full-body double | Advertised high; untested independently | UNVERIFIED (launch April 2026) | Reallusion EULA; likeness rights are your responsibility | Via CC5 | CC formats | AI model; AI Image Generator (Nano Banana Pro) paid add-on | Rapid likeness, cleanup of phone photos | AI misses details (vendor admits presets needed) | Windows, GPU | Optional | Medium | beforesandafters.com/2026/04/30/...headshot-3...; cgchannel.com/?p=175693 (2026-10-09) |
| AccuRIG | Auto-rigger | Reallusion | https://www.reallusion.com/auto-rig/accurig/ | Rig any humanoid mesh | Good body rig; no facial rig in free version | Free | Reallusion EULA (UNVERIFIED specifics) | FBX/USD to UE | FBX, USD | None documented | Handles scan poses, fingers, accessories | No face rig, bone or mesh reduction in free version | Windows | Optional | High | reallusion.com/auto-rig/accurig (2026-10-09) |
| ActorCore (characters) | Scanned-people store | Reallusion | https://actorcore.reallusion.com/ | Rigged scanned crowd characters (and mocap) | Medium–high (scan-based) | Per item (UNVERIFIED) | Reallusion content licence | FBX to UE | FBX | None | Ready crowd people | Licence limits on runtime randomization; generic look risk | n/a | Optional | Medium | actorcore.reallusion.com; reallusion.com/license/content.html (2026-10-09) |
| iClone 8 + AccuFace 2 | Facial mocap (webcam) / animation | Reallusion | https://www.cgchannel.com/2026/07/reallusion-launches-accuface-2/ | Webcam/video face capture to CC characters; Live Link to UE | Good (CC5 HD profile, 128 lip morphs) | iClone $599 perpetual or $199/yr; AccuFace 2 $299; iClone Personal free (per CGChannel) | Reallusion EULA | iClone Unreal Live Link (reportedly free under $100K revenue, UNVERIFIED) | iClone, FBX | None documented | Cheap webcam capture, AMD support | Windows-only; CC-centric | 720p30 webcam; NVIDIA or AMD RX 5700+ | Optional | Medium | cgchannel.com/2026/07/reallusion-launches-accuface-2 (2026-10-09) |
| Daz Studio + Genesis 9 + Daz to Unreal | Character creator | Daz 3D | https://www.daz3d.com/interactive-license-info | Figure/morph-based characters | Medium–high stills; game suitability UNVERIFIED | Studio free (UNVERIFIED); Interactive License per item (~$10–50 user-reported, UNVERIFIED) | Standard EULA = renders only; Interactive License required for games, if offered | Bridge lists UE 5.1/5.2; newer UNVERIFIED | FBX via bridge | Daz Script (UNVERIFIED) | Huge content library | Per-item licensing; bridge lag; recognizable stock look (opinion) | Windows/macOS | Avoid | Medium | daz3d.com/interactive-license-info; delorean.daz3d.com/unreal-bridge (2026-10-09) |
| MPFB 2 (MakeHuman for Blender) | Open-source human generator | MakeHuman community | https://static.makehumancommunity.org/mpfb/faq/use_in_closed_source.html | Parametric base humans in Blender | Medium (not photoreal by default) | Free | Assets CC0; code GPL | FBX export to UE (UNVERIFIED specifics) | Blender, FBX | Blender Python | Zero licence risk | Far below MetaHuman realism; manual skin and hair work | Any | Optional | High (licence) / Low (quality) | static.makehumancommunity.org MPFB FAQ; cgchannel.com/2025/03/... (2026-10-09) |
| Blender (character work) | DCC | Blender Foundation | https://www.blender.org | Sculpt, retopo, hard-surface gear, grooms | Pro-capable | Free | GPL (outputs yours) | FBX/Alembic/USD to UE | FBX, Alembic, USD | Python (bpy); MCP covered by other specialist | Free; MetaHuman FBX round-trip (5.7) | Manual skill needed | Any | Valuable | Medium | metahuman.com/news/metahuman-5-7-brings-major-improvements-to-body-conforming... (2026-10-09) |
| ZBrush | Sculpting | Maxon | UNVERIFIED | Unique head/body sculpts, detail | Industry standard | UNVERIFIED | UNVERIFIED | Via FBX round-trip / GoZ (CC) | FBX, OBJ, GoZ | ZScript (UNVERIFIED) | Best-in-class sculpting | Cost / subscription (UNVERIFIED) | Workstation | Optional | Low | Not researched; see modeling specialist |
| Faceform Wrap | Scan wrapping / retopo | Faceform (formerly R3DS) | https://faceform.com/wraporiginal/ | Fit clean base topology to scans; blendshape transfer | Pro-grade (film and game standard; inference) | Indie $570 perpetual (under $100K revenue); Pro $1,070 node-locked / $1,470 floating | Commercial tool licence (EULA UNVERIFIED) | Indirect (outputs FBX/OBJ for Mesh to MetaHuman) | OBJ, FBX (UNVERIFIED list) | Node graph; batch across scans; scripting API UNVERIFIED | Repeatable batch processing of scans | Learning curve; Windows/Linux only | Workstation | Valuable (if scanning) | High | cgchannel.com/2025/12/faceform-releases-wrap-2025-11-and-wrap4d-2025-11; faceform.com/wraporiginal (2026-10-09) |
| 3D Scan Store | Head/body scan library | Ten24 / 3D Scan Store | https://www.3dscanstore.com | Licensed scans, expression packs, MetaHuman identities | High (photogrammetry) | Per product (UNVERIFIED) | Business Commercial Project Licence required for commercial games; free scans personal-only | Via Mesh to MetaHuman; 69-identity MetaHuman pack | OBJ/FBX/textures (UNVERIFIED) | None | Real-human detail and diversity | Must buy the commercial licence tier | n/a | Valuable | High (licence) | 3dscanstore.com/studios-and-commercial-customer-information; 80.lv 69 MetaHumans (2026-10-09) |
| Marvelous Designer 2025.2 | Clothing simulation/authoring | CLO Virtual Fashion | https://support.marvelousdesigner.com/hc/en-us/articles/52699135975705 | Suits and tactical garments for MetaHuman Outfit Assets | Industry standard for garments | UNVERIFIED (see modeling specialist) | UNVERIFIED | USD to MetaHuman Outfit; ChaosCloth: MD 2025.2 with UE 5.6+, not 5.5 | USD, FBX, .dna import | Python API UNVERIFIED | Imports MetaHuman .dna; garments auto-resize in MHC | Version-locked ChaosCloth compatibility | Workstation | Essential (hero/cast clothing) | High | support.marvelousdesigner.com article; cgchannel.com/2025/11/clo-virtual-fashion-releases-marvelous-designer-2025-2 (2026-10-09) |
| UE Groom + Hair Card Generator | Hair system | Epic Games | https://dev.epicgames.com/documentation/unreal-engine/creating-hair-cards-and-lods-using-hair-card-generator | Strands, cards and mesh LODs | Strands cinematic; cards game-grade | Free | UE EULA | Native | Alembic in | Dataflow / Python (UNVERIFIED) | Generate cards from your strand groom | Strand cost; no official ms figures found | GPU-heavy for strands | Essential | Medium | Epic docs via search snippet (2026-10-09) |
| Animation Budget Allocator | Crowd animation LOD | Epic Games | https://dev.epicgames.com/community/learning/tutorials/2JVl/unreal-engine-metahuman-skeletal-mesh-optimization-guide | Throttle skeletal anim cost by budget | n/a | Free | UE EULA | Native plugin | n/a | Console vars / Blueprint | Big CPU savings for many MetaHumans | Needs component class swap | n/a | Valuable | Medium | EDC community tutorial (2026-10-09) |
| OmniFaceRig (research) | AI facial auto-rig | Meta Reality Labs (per search) | https://arxiv.org/pdf/2606.08043 | Auto FACS rig (155 blendshapes) plus inner mouth | Research-grade | n/a | Paper / benchmark (licence UNVERIFIED) | None | n/a | Research code status UNVERIFIED | Promising automation | No production or engine validation | n/a | Avoid (for production now) | Low | arxiv.org/pdf/2606.08043 (2026-10-09) |

### Takeaway
The table covers 23 tools. Essential tools for this project are MetaHuman Creator, Mesh to MetaHuman, MetaHuman Animator, Live Link Face, the UE Groom system and Marvelous Designer.

### Cited Findings
Each row's sources are listed in the row.

### Inferences
Confidence is "High" only where primary vendor pages were read in full this session.

### Gaps
Pricing for ZBrush, Marvelous Designer, Headshot 3 and ActorCore items is not verified here.

---

## 11. Unresolved questions

### Takeaway
The main open items are:
- Exact AI-clause wording in the UE EULA, which matters for AI-agent workflows that show MetaHuman renders to an LLM.
- Collections performance numbers and production readiness.
- Reallusion's stale or contradictory licence page text.
- Official MetaHuman per-LOD budgets.

### Cited Findings
- The UE EULA change log confirms the AI restriction exists but not its exact text ([change log](https://www.unrealengine.com/eula-change-log/unreal)).
- The Reallusion content page mixes the "Expanded Standard License" with legacy "Extended License" FAQ text ([reallusion.com/license/content.html](https://www.reallusion.com/license/content.html)).
- Collections is "Experimental" ([metahuman.com 5.8](https://www.metahuman.com/news/metahuman-5-8-is-now-available)).

### Inferences / open questions for the report writer
1. **AI and MetaHuman.** Does sending MetaHuman screenshots or renders to Claude (vision) for automated visual QA count as "prompt-based input into Generative AI Programs"? The change log suggests the restriction targets programs that train on inputs. Anthropic's commercial API data policy is outside my scope. Ask Epic or legal before building agent-vision QA on MetaHuman renders. Writing Python that manipulates MetaHumans appears unaffected.
2. **Collections maturity.** Is MetaHuman Collections production-ready in 5.8, and will 5.9 promote it from Experimental? There is no 5.9 MetaHuman information yet.
3. **Performance figures.** I found no official per-LOD triangle and joint counts, optimized-assembly tier names, or strand vs card GPU ms for 5.6–5.8 (Epic docs blocked). These need in-engine profiling.
4. **Reallusion licence text.** Get written confirmation from Reallusion that (a) the "Expanded Standard License" fully replaces Extended for games, and (b) a pre-generated pool of CC characters assembled into crowds at runtime by a randomizer counts or does not count as "in-software character generation".
5. **CC to MetaHuman conversion.** Is converting a CC5 character to a MetaHuman via Mesh to MetaHuman allowed under Reallusion's AP EULA §5 (topology ownership, transformation clauses)?
6. **Daz.** Does the current Daz to Unreal Bridge support UE 5.7/5.8? What are the current Interactive License prices for Genesis 9 base figures?
7. **Headshot 3 and ActorCore.** Prices, and independent quality tests of Headshot 3 output in UE.
8. **Fab EULA wording.** Does it treat MetaHuman outfit or groom listings differently from the MetaHuman Content Addendum's "UE-Only Content" line? The conflict is irrelevant for UE-only shipping but should be noted.
9. **Rigid gear.** Do MetaHuman Outfit Asset resizing and MD 2025.2 ChaosCloth handle rigid tactical gear (plate carriers) without manual per-body fixes?
