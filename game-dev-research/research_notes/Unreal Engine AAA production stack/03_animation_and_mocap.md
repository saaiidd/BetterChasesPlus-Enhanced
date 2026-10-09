# Specialist 3 of 12: Animations, Motion Capture, and Rigging for an original photorealistic UE5 third-person espionage/action game

Research date: 2026-10-09. All URLs accessed 2026-10-09 unless noted. Benchmarks (Hitman WoA, 007 First Light, GTA V Enhanced) are quality references only; nothing here copies them. Several dev.epicgames.com documentation pages returned HTTP 403 to the research tools (Motion Warping, Distance Matching, GASP docs, Motion Matching docs). Where that happened, evidence comes from Epic's Python/Blueprint API reference pages, Epic blog posts, Epic forum staff answers, and Fab listings.

Headline facts for the report writer:
- **UE 5.8 has shipped.** The release thread was posted 2026-06-17 and the news article is dated 2026-06-23. Epic calls it "the last planned major Unreal Engine 5 release" and says UE6 work is now ramping up.
- **Epic now gives away free markerless mocap.** The MetaHuman Animator Markerless Motion Capture plugin captures body, hands and face from one webcam or phone. It is Experimental, Windows-only, processes locally, and is free including commercial use.
- **The Game Animation Sample (GASP) was updated for 5.8 on 2026-08-12.** The update adds multi-character motion-matched shoves, tackles and takedowns, a powered-ragdoll character with motion-matched get-ups, and bench interactions. Its licence is UE-Only, so it can ship in a UE game.
- **Two AI-mocap vendors dropped out in 2026.** RADiCAL's standalone platform shut down on 2026-07-06 after Autodesk bought its core technology. The Kinetix team joined Runway on 2026-09-08.
- **Mixamo still works.** It remains free and royalty-free for games, but it is unmaintained.

---

## 1. Findings per tool/library (evidence + URLs)

### Takeaway
A solo developer can build a credible AAA-style locomotion foundation entirely free. That foundation is UE 5.8's built-in systems (Motion Matching/Pose Search, IK Rig/Retargeter, Control Rig, warping nodes) plus GASP's 500+ animations, the free MetaHuman markerless capture plugin, and CC0 libraries.

Genre-specific espionage content still has to be bought on Fab or captured yourself. That means armed locomotion, reloads, cover, stealth takedowns and vehicle entry. Cleanup tools such as Cascadeur or Control Rig are what turn bought or captured data into AAA-feeling results.

### Cited Findings

#### A. Unreal Engine built-in systems (UE 5.7–5.8)

UE 5.8 release and status:
- UE 5.8 is available. Epic calls it "the last planned major Unreal Engine 5 release on our roadmap as we ramp up work on UE6", with continued bug-fix support. — [Epic news, 2026-06-23](https://www.unrealengine.com/news/unreal-engine-5-8-is-now-available); release thread dated 2026-06-17 — [Epic forum](https://forums.unrealengine.com/t/unreal-engine-5-8-released/2729274)
- UE 5.8 animation and rigging changes:
  - Control Rig Physics moves to Beta.
  - A new runtime particle-based "Control Rig Dynamics" solver runs about 5x faster than the original solver.
  - Direct Mesh Controls (Experimental).
  - Automated one-click animation baking.
  - New foot definition options for retargeting, plus "Retarget Override Sets" that reduce rework across characters of different proportions.
  - Live Link Hub is now Production-Ready.
  - A free "Zebra Character Sample" ships a production body rig that uses the same rigging modules as Fortnite characters.
  - Source: [Epic news](https://www.unrealengine.com/news/unreal-engine-5-8-is-now-available)
- UE 5.8 adds an Experimental MCP (Model Context Protocol) plugin, so LLMs can connect to the engine and project, including Blueprints, assets, levels, materials and meshes. This overlaps with the MCP/automation specialist. — [Epic news](https://www.unrealengine.com/news/unreal-engine-5-8-is-now-available)

Mover plugin:
- Mover is still Experimental in the 5.8 docs: "use caution when shipping with it." Epic intends it to eventually replace the Character Movement Component (CMC) and says CMC will stay supported "for the foreseeable future." — [Epic docs: Mover](https://dev.epicgames.com/documentation/unreal-engine/mover-in-unreal-engine); [Mover API 5.8](https://dev.epicgames.com/documentation/unreal-engine/API/Plugins/Mover) (the plugin lives under /Plugins/Experimental/; the API includes `UMotionWarpingMoverAdapter`)
- An Epic staff forum answer (June 2026) says they are "shooting for Mover/ChaosMover to be in shape for Beta this year". Breaking API changes are expected first, for example the layered-move rework. — [Epic forum](https://forums.unrealengine.com/t/question-about-the-next-steps-for-chaos-mover/2743117)
- Another staff answer (Feb 2026) says there are "no guarantees against data loss or API breakage with Experimental features" and "I fully expect breaking API changes between now and Beta." — [Epic forum](https://forums.unrealengine.com/t/mover-plugin-roadmap-and-layering-issues/2712958)

Unreal Animation Framework (UAF):
- UAF is the "future replacement for Anim Blueprints". Epic planned a first real look and a GASP character "set up entirely using UAF" for 5.8, still Experimental. — [Epic GASP 5.7 blog, 2025-12-03](https://www.unrealengine.com/en-US/tech-blog/explore-the-updates-to-the-game-animation-sample-project-in-ue-5-7)
- The 5.8 GASP blog (2026-08-12) does not mention a UAF character. See Gaps. — [Epic GASP 5.8 blog](https://www.unrealengine.com/tech-blog/download-the-latest-game-animation-sample-project-now-updated-for-ue-5-8)

Motion Matching / Pose Search:
- Motion Matching "continuously selects the best frame of animation to play from its motion database" using the current pose and past and future trajectory. It "uses Pose Warping algorithms to fill in the gaps" and also covers jumping, falling and traversal. — [Epic blog: GASP launch (UE 5.4 era, 2024)](https://www.unrealengine.com/blog/game-animation-sample)
- 5.7 adds an Experimental Pose Search Column in Choosers. — [GASP 5.7 blog](https://www.unrealengine.com/en-US/tech-blog/explore-the-updates-to-the-game-animation-sample-project-in-ue-5-7)
- 5.8 adds multi-character motion matching and "Pose Search Interaction Assets" for multi-character animations, "complete with tooling for easy motion warp handling", searched via Motion Match Multi functions. — [GASP 5.8 blog](https://www.unrealengine.com/tech-blog/download-the-latest-game-animation-sample-project-now-updated-for-ue-5-8)

IK Rig / IK Retargeter:
- The retargeter is now op-stack based. `add_default_ops()` adds "Pelvis Motion, FK Chains, IK Chains, IK Solve and Root Motion". Other scripting calls include `auto_map_chains`, `set_source_chain` and `snap_bone_to_ground`. — [Unreal Python 5.8: IKRetargeterController](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/IKRetargeterController)
- Batch retargeting is scriptable through `IKRetargetBatchOperation.run_batch_retarget(inputs)`. Inputs include source/target mesh, retargeter, override set names, prefix/suffix and `retain_additive_flags`. — [Python: IKRetargetBatchOperation](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/IKRetargetBatchOperation); [IKRetargetBatchOperationInputs](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/IKRetargetBatchOperationInputs)
- `BatchRetargetSettings.auto_generate_retargeter` "will attempt to generate an IK Retargeter" and auto-align retarget poses. It is "limited to common, predefined skeleton types that Unreal knows about". For other skeletons you must supply a custom retargeter. — [Python: BatchRetargetSettings](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/BatchRetargetSettings)
- The pre-5.6 UObject op stack is deprecated ("Use RetargetOps instead"). Root lock, export and speed-plant settings exist per chain and profile. — [Python: IKRetargeter](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/IKRetargeter)

Animation warping and distance matching (fixes for foot sliding):
- Distance Matching "effectively changes the play rate of the animation to keep the feet from sliding". The default play-rate clamp is [0.75, 1.25]. `distance_match_to_target` is used for stops without foot sliding and needs a distance curve on the animation. — [Python: AnimDistanceMatchingLibrary 5.7](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/AnimDistanceMatchingLibrary?application_version=5.7)
- Stride Warping computes StrideScale = LocomotionSpeed / RootMotionSpeed. It has foot definitions (IK, FK, thigh) and a pelvis pull-down solver, and can clamp IK using FK limits. — [Python: AnimNode_StrideWarping](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/AnimNode_StrideWarping?application_version=5.4)
- Orientation Warping "maintains a look at direction for the upper body, while rotating the lower body to match capsule velocity direction". — [Python: AnimNode_OrientationWarping 5.7](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/AnimNode_OrientationWarping?application_version=5.7)
- Epic's Lyra blog (2022, older but still the clearest statement) says "the industry standard of playrate/stride warp adjustments is of 15-20% max increase/decrease. Values beyond that result in significant visual quality drops, and it's often better to allow foot sliding." Distance curves can be added in bulk with the Distance Curve Animation Modifier. — [Epic tech blog: Adapting Lyra animation, 2022-04-26](https://www.unrealengine.com/tech-blog/adapting-lyra-animation-to-your-ue5-game)
- 5.7 added a Footplacement Control Rig node "as a first step" toward Control Rig and UAF integration. — [GASP 5.7 blog](https://www.unrealengine.com/en-US/tech-blog/explore-the-updates-to-the-game-animation-sample-project-in-ue-5-7)

Motion Warping:
- In 5.8, the Motion Warping component can create warp targets from a scene component or bone. It has a "Follow Component" option that updates the target while warping is active, "useful for tracking moving targets". — [UE 5.8 Blueprint API: Add or Update Warp Target from Component](https://dev.epicgames.com/documentation/en-us/unreal-engine/BlueprintAPI/MotionWarping/AddorUpdateWarpTargetfromCompone-)
- The overview docs page returned 403, so the maturity label is not confirmed. — [Epic docs (403)](https://dev.epicgames.com/documentation/unreal-engine/motion-warping-in-unreal-engine?lang=en-US)

Physics-driven animation:
- The 5.8 GASP adds the Physics Control Component and Physics Control Asset, which drive physics assets through linear and angular motors for powered ragdolls. — [GASP 5.8 blog](https://www.unrealengine.com/tech-blog/download-the-latest-game-animation-sample-project-now-updated-for-ue-5-8)
- An Experimental "Locomotor" plugin for procedural multi-foot placement is mentioned by a community moderator, not Epic staff. — [Epic forum](https://forums.unrealengine.com/t/mover-2-0-upgrade-path/2691288)

#### B. Epic Game Animation Sample Project (GASP)
Detailed in Section 2. In brief: free on Fab, 500+ animations, supports UE 5.4–5.8, UE-Only licence. — [Fab listing](https://www.fab.com/listings/880e319a-a59e-4ed2-b268-b32dac7fa016)

#### C. MetaHuman Animator Markerless Motion Capture Plugin (new in 2026)

What it does:
- MetaHuman Animator "can now capture your full character performance: face, body, or both at once, from a single off-actor camera". Processing is offline on your own machine. It is "Introduced as Experimental" and comes "from the integration of Meshcapade's Markerless Motion Capture". The plugin is on Fab for Windows. — [metahuman.com, 2026-06-11](https://www.metahuman.com/news/metahuman-5-8-is-now-available)
- Epic's Fab listing text, as posted in the forum: it generates "full-body animation, including hands… ready to drive the MetaHuman skeleton directly or retarget to other character types". It is local only ("Your footage never leaves your workstation"), and "Scripted Automation… through Python and Blueprints". — [Epic forum announcement, 2026-06-17](https://forums.unrealengine.com/t/epic-games-metahuman-animator-markerless-motion-capture-plugin/2729293)

Licence and requirements:
- It is free, including for commercial projects. It requires UE 5.8 on Windows. — [CG Channel, 2026-06-18](https://www.cgchannel.com/2026/06/get-the-free-metahuman-animator-markerless-mocap-plugin/)
- Requirements: UE 5.8+, the MetaHuman Animator plugin, Windows only, and DX12.
- Recommended hardware: 16 physical cores, 32 GB RAM, and a GPU such as an RTX 3070 or RX 6800 XT with 8 GB VRAM.
- No accuracy metrics, solve times, occlusion behaviour or multi-actor support had been published at the time.
- Source: [Digital Production, 2026-06-23](https://digitalproduction.com/2026/06/23/metahuman-gets-markerless-mocap/)

Hands-on reports:
- An early tester reported "some floating feet which is very much expected" and fingers that were surprisingly good, but worse results from moving cameras. — [CG Channel](https://www.cgchannel.com/2026/06/get-the-free-metahuman-animator-markerless-mocap-plugin/)
- A tutorial vendor (secondary source, but hands-on) reports:
  - About 6 GB of MetaHuman Core Data plus about 5 GB for the plugin.
  - Processing takes "15–30 minutes for a 30 seconds clip".
  - Minimums are 8 GB VRAM and 32 GB RAM.
  - Quality is best when the subject stays grounded; they say it "can look close to a Rokoko suit capture" (opinion).
  - Source: [CGDive guide](https://addons.cgdive.com/tools/unreal-engine-markerless-mocap)

#### D. Mixamo (Adobe)
- Adobe's official licence FAQ, posted on the Adobe community forum in 2022, says Mixamo is "available for free, with no licensing or royalty fees, for unlimited commercial or non commercial use".
  - Allowed: video games and DLC.
  - Not allowed: distributing "character or animation raw files" (asset packs or templates), or "training machine-learning models".
  - No attribution is required.
  - Use is "covered under the terms at the time you use it."
  - Source: [Adobe Community: Mixamo FAQ – Licensing](https://community.adobe.com/questions-696/mixamo-faq-licensing-royalties-ownership-eula-and-tos-589400)
- The official helpx FAQ page shows "Last updated on Sep 14, 2021". — [Adobe helpx Mixamo FAQ](https://helpx.adobe.com/creative-cloud/faq/mixamo-faq.html)
- Status in 2026: a Sept 2026 third-party guide says Mixamo still works, free with an Adobe ID, with no significant updates since the 2015 acquisition.
  - It also reports a multi-day outage in June 2025.
  - A forum user says a support agent told them Mixamo "is not supported anymore". That is UNVERIFIED and is not an official Adobe statement.
  - Source: [Cinevva guide (vendor blog)](https://app.cinevva.com/guides/free-character-animations-rigging); [Adobe community thread](https://community.adobe.com/questions-696/mixamo-not-working-589817)

#### E. Reallusion: ActorCore, AccuRIG 2, iClone 8

ActorCore:
- ActorCore is a pay-per-content store, not a subscription. It sells "export license" content. — [Reallusion forum](https://forum.reallusion.com/PrintTopic462176.aspx)
- Content covers mocap and hand-keyed motions, stunt mocap, interpersonal/conversational interactions, and "MotionPlus" prop-paired motions. Characters, motions and props can be combined into a single FBX or USD. — [ActorCore motion technology page](https://actorcore.reallusion.com/3d-motion-technology?a=)

Content licence:
- The Standard License now allows export "to any external software or game engine" and use "for mass distribution in… game titles". It is royalty-free and perpetual.
- The iContent licence (30% of the Standard price) only covers use inside Reallusion apps and does not allow export.
- Enterprise is needed for AI training or embedding.
- CC Components used on characters in commercial games need an Extended licence. That is the character side, which overlaps with the character specialist.
- Source: [Reallusion Content License Policy](https://www.reallusion.com/license/content.html)

AccuRIG 2:
- AccuRIG is free. It rigs one-mesh or multi-mesh A/T-pose humanoids and exports FBX or USD with target presets including Unreal. — [AccuRIG 2 manual: Export](https://manual.reallusion.com/AccuRig-2/2.0/09-add-motions/export.htm); [ActorCore AccuRIG page](https://actorcore.reallusion.com/auto-rig)
- Built-in library access is variously stated as "over 4,500" motions ([AccuRIG 2 manual](https://manual.reallusion.com/AccuRig-2/2.0/01-welcome/welcome.htm)) or "over 3,000 retargetable motions" ([ActorCore upload page](https://actorcore.reallusion.com/static-page/auto-rig/pre_page/upload-export/upload-export.html?476232=)). The two Reallusion pages conflict.
- Free AccuRIG lacks skin-weight editing and bone reduction; those need Character Creator. — [ActorCore AccuRIG page](https://actorcore.reallusion.com/auto-rig)

iClone 8:
- iClone 8 (perpetual or iClone 365 subscription) includes "AI Video Mocap" and AccuPOSE, an Unreal Live Link, and Motion LIVE profiles for Xsens, Vicon, OptiTrack, Perception Neuron and Rokoko. — [Reallusion perpetual pricing page](https://www.reallusion.com/plan-and-pricing/individual/perpetual); [Reallusion subscription page](https://www.reallusion.com/plan-and-pricing/individual/subscription)
- Exact prices did not render. UNVERIFIED.

#### F. Cascadeur (Nekki)

Plans:

| Plan | Price | Export | Commercial use |
|---|---|---|---|
| Free | $0 | `.casc` only (no FBX/DAE) | "Only non-commercial use" |
| Indie | $19/month, or $8/month billed annually | FBX, DAE, USD, GLTF | Allowed while "revenue must be less than $100k per year" |
| Pro | $49/month, or $33/month billed annually | FBX, DAE, USD, GLTF | "Commercial use with no limits" |
| Teams | $33 per user per month (annual) | FBX, DAE, USD, GLTF | As Pro |

- Pro also adds "Animation retargeting", "Interaction with environment" in AutoPhysics, and the scene-linking tool.
- Yearly Indie and Pro plans give perpetual-licence builds of every major version released during the subscription.
- Source: [Cascadeur plans](https://cascadeur.com/plans)

Cascadeur 2026.1 (2026-04-09):
- A rebuilt Unreal Engine Live Link, for paid subscribers only. Docs say UE 5.5–5.7; CG Channel says 5.5+. Installation is manual, with Fab distribution planned.
- Filament renderer.
- An AI "Root Motion" generative tool (diffusion) with a reference/style mode.
- Collision Penetration Cleaning.
- Quick-rigging templates for meshy.ai and Auto-Rig Pro armatures.
- Sources: [Cascadeur blog](https://cascadeur.com/blog/view/cascadeur-2026-1-new-renderer-ue-live-link); [Digital Production](https://digitalproduction.com/2026/04/09/cascadeur-2026-1-adds-unreal-live-link-filament-rendering-and-ai-root-motion/); [CG Channel](https://www.cgchannel.com/2026/04/nekki-releases-cascadeur-2026-1/)

Cascadeur 2026.2:
- Additive Animation Layers (Alpha) and easing controls.
- Inbetweening and Motion Generation (formerly Root Motion) were "trained on larger datasets".
- Collision Penetration Cleaning is now included by default in AutoPhysics ground passes.
- Python API improvements.
- Source: [Cascadeur blog 2026.2](https://cascadeur.com/blog/view/cascadeur-2026-2-animation-layers-easing-new-languages)

The 2026.1 help index lists "Mocap (Alpha)", but its scope is UNVERIFIED. — [Cascadeur help 2026.1](https://cascadeur.com/help/category/312)

#### G. Rokoko (inertial suits + Vision AI + Studio)

Rokoko Studio software plans:

| Plan | Price (annual / monthly billing) | Vision AI video processing | Notable features |
|---|---|---|---|
| Starter | Free | 30 s/month | Record and export FBX |
| Basic | $10 / $12 per month | 600 s/month | Custom character import and retarget, BVH |
| Plus | $20 / $28 per month | 3000 s/month | Live stream to third-party tools, including the UE plugin |
| Pro | $50 / $70 per month | 15000 s/month | Face Capture, "Advanced command API", UDP triggers |
| Enterprise | $100 / $150 per month | Not stated in the notes | Custom solutions |

- The Motion Library is usable "in any of your projects, including commercial ones."
- Source: [Rokoko pricing](https://www.rokoko.com/pricing)

Rokoko hardware:
- Smartsuit Pro II (Glove Ready): $2,295. — [Rokoko store](https://store.rokoko.com/products/smartsuit-pro-ii-glove-ready)
- Coil Pro: $2,995. — [Rokoko store](https://store.rokoko.com/products/coil-pro)
- Smartgloves II + Coil Pro bundle: $4,795. — [Rokoko](https://www.rokoko.com/products/smartgloves-coil-pro-precision-finger-capture)
- Full Performance Capture bundle (suit, gloves, Headcam or Headrig, one year of Studio Pro): $4,495. — [Rokoko](https://www.rokoko.com/products/full-performance-capture)
- Indie bundles save "up to 30%" for buyers under $100k annual revenue with 3 or fewer full-time employees. — [Rokoko indie bundle](https://www.rokoko.com/mocap/indie-creator-bundle)
- The Coil Pro removes root drift using EMF absolute positioning within roughly a 5 m tracking radius. Outside that range, the suit falls back to IMU tracking. — [Rokoko Coil Pro](https://www.rokoko.com/products/coil-pro)

#### H. Move AI

Current pricing page (crawl date unknown; page footer says 2025):

| Plan | Price | Credits | Notes |
|---|---|---|---|
| Free | $0 | 30 lifetime | |
| Starter | $15/month | 180 | |
| Basic | $35/month | 480 | |
| Team | $225/month | 4000 | 3 seats |
| Enterprise | Custom | Custom | Includes "API" and desktop access |

- Move One limits: 1 person, 5 m x 5 m volume, HD, 60 fps, 60 s capture limit, hand and finger tracking. Exports FBX, USD and MP4.
- Move Pro: up to 8 people, a 20 m x 20 m volume, 4K and 120 fps.
- Move Live: on-premise, 4–8 cameras, up to 4 people, 110 fps, 100 ms latency, and an Unreal plug-in.
- Source: [Move AI pricing](https://www.move.ai/pricing)

Move AI's docs page shows a different, apparently older, structure:

| Plan | Price | Credits | Licence |
|---|---|---|---|
| Personal | $15/month | 2,250 | "Non-commercial use" |
| Standard | $50/month | 7,500 | "Commercial use allowed" |
| Advanced | $250/month | 37,500 | Commercial; adds the Gen 2 model |
| Move Pro 1-month trial | $995 | 1000 credits, about 1,000 s | Commercial; up to 4 cameras, 1 actor |

- Credit burn: the s1 model uses 6 credits per second and s2 uses 12.
- An API is available.
- Source: [Move AI docs: Pricing](https://docs.move.ai/knowledge/move-ai-pricing-plans-credits)
- **Conflict:** the two official Move AI pages disagree on tiers and on commercial rights. Commercial rights for the current Starter and Basic tiers are UNVERIFIED.

#### I. DeepMotion (Animate 3D / SayMotion)
- 1 credit equals 1 second of animation. Face and hand tracking each cost an extra 0.5 credit per second. — [DeepMotion pricing FAQ](https://www.deepmotion.com/pricing-animate3d)
- The Freemium tier gives "60 seconds of animations every month" and is "personal, non-commercial use". "In order to have licensed animation, you must have a premium subscription plan." — [DeepMotion pricing FAQ](https://www.deepmotion.com/pricing-animate3d)
- The Studio plan gives unlimited credits; the first 7,200 credits each month get high processing priority. API plans are available. — [DeepMotion pricing FAQ](https://www.deepmotion.com/pricing-animate3d)
- Exact plan prices could not be confirmed from DeepMotion's own page (it renders with JavaScript). Third-party figures conflict, for example Starter at about $15/month. UNVERIFIED.

#### J. Autodesk Flow Studio (formerly Wonder Studio)
- Tiers are Free, Lite, Standard, Pro and Enterprise.
- Monthly credits are 300, 2,100, 6,000 and 12,000 respectively (Free through Pro). Enterprise gets 48,000 credits and 7-person mocap. AI mocap costs 10 credits per second.
- It exports FBX with body and hand tracking. Face tracking goes out through a Blender scene, and whole scenes can be exported to Unreal Engine or USD.
- Enterprise has "AI data opt-out enabled by default".
- Sources: [Autodesk compare page](https://www.autodesk.com/eu/products/flow-studio/compare); [Autodesk Flow Studio buy page](https://www.autodesk.com/products/flow-studio/buy)
- The paid tiers are needed "to use Flow Studio's AI mocap in your projects". Autodesk mentions "automatic foot locking, smoothing, and trajectory fixes" and, in 2026, "Enhanced AI Motion Capture… reduced foot sliding" from fixed-camera shots. — [Autodesk AI mocap page](https://www.autodesk.com/solutions/media-entertainment/ai-motion-capture-with-flow-studio); [Autodesk features](https://www.autodesk.com/products/flow-studio/features)
- Dollar prices did not render; the page shows template placeholders. UNVERIFIED.

#### K. Other markerless/AI mocap: status in Oct 2026

RADiCAL is dead as a product:
- Autodesk acquired RADiCAL's core technology and IP in April 2026, and the web portal closed on 2026-07-06. User data was not transferred. — [CG Channel, 2026-04-09](https://www.cgchannel.com/2026/04/autodesk-acquires-core-tech-of-ai-motion-capture-firm-radical/); [RADiCAL LinkedIn](https://www.linkedin.com/posts/radicalmotion_were-excited-to-share-that-autodesk-has-activity-7449449797721346048-68cb)

Kinetix has been absorbed:
- The Kinetix team joined Runway on 2026-09-08 to work on world models and robotics. — [Runway news](https://runway.com/news/company-news/kinetix-joins-runway); [Dealroom](https://dealroom.co/news/149523-runway-buys-frances-kinetix-crosses-200m-arr-in-physical-ai-push/)
- Whether Kinetix's consumer animation product still runs: UNVERIFIED.

Plask is alive:
- The site advertises "upgraded motion AI and rendering pipeline" and exports to Unreal, Unity, Maya and Blender. — [plask.ai](https://plask.ai/)
- Pricing (a free tier of 15 s per day; Pro with one hour per month) comes from a secondary article citing an aggregator. UNVERIFIED. — [Startuply.vc, 2026-06-17](https://startuply.vc/article/plask-s-browser-tab-replaces-the-motion-capture-studio-wu82qj)

Uthana is alive and pay-as-you-go:

| Capability | Price |
|---|---|
| Text-to-motion | $0.02–$0.10 per second, depending on model version |
| Video-to-motion | $0.05 per second |
| Locomotion | $0.02 per second |
| Auto-rig | $0.15 per rig |

- Uthana "stopped offering new subscriptions in August 2026". — [Uthana pricing](https://uthana.com/pricing)
- It has a GraphQL API, plugins for Blender and Maya, FBX/GLB/BVH export, and a UE5 FBX preset. Text-to-Motion 3.0 clips are 4–10 s long. — [Uthana FAQ](https://uthana.com/faq); [Uthana API docs](https://uthana.com/docs/api/capabilities/text-to-motion)
- Its commercial licence terms were not found. UNVERIFIED.

Autodesk MotionMaker (in Maya since June 2025):
- An AI "autoregressive motion generator" for locomotion. It ships pre-trained human (male and female), canine and horse models, a foot-slide reduction tool, and "Bring Your Own Data" training. Requires Maya. — [Autodesk blog, 2025-06-04](https://blogs.autodesk.com/media-and-entertainment/2025/06/04/meet-motionmaker/)

#### L. Libraries and marketplaces (Fab and others)

Quaternius Universal Animation Library (CC0):
- UAL1 (March 2025) has 120+ animations, including 8-direction locomotion, jog, sprint, crawling, sitting, combat and gun, and deaths. UAL2 (January 2026, v2.0 dated 2026-06-16) has 130+ more, including melee combos, parkour and zombie locomotion. — [Quaternius UAL](https://quaternius.com/packs/universalanimationlibrary.html); [UAL2](https://quaternius.com/packs/universalanimationlibrary2.html); [itch.io UAL2](https://quaternius.itch.io/universal-animation-library-2)
- Both are licensed CC0, so they are free for commercial use. They come in root-motion and in-place versions.
- The free "Standard" version has 45 animations; the full Source version comes from the author's site. — [OpenGameArt](https://opengameart.org/content/universal-animation-library)

MoCap Online:
- Rifle Pro has 390+ rifle animations: 8-direction walk, jog and run, both rifle-up and rifle-down, plus crouch, aim offsets, fire, reload, swaps and holsters.
  - It is delivered on the UE4 skeleton with FBX sources and custom IK Retargeters for the UE5 Manny.
  - It includes root-motion and in-place versions, and its Fab listing says UE 4.14–5.6.
  - Source: [Fab: Rifle Shooter Pro](https://www.fab.com/listings/6cbfcfb5-e96b-4a69-b8f2-124844c17b8f); [mocaponline.com Rifle](https://mocaponline.com/products/rifle)
- Pistol Pro has 370+ animations with similar coverage. — [Fab: Pistol Pro](https://www.fab.com/listings/bc953490-c2e1-4be6-923a-02ad5fc7a89a?lang=en)

Kubold:
- Cover Animset Pro has 62 rifle cover animations plus 43 aim-offset poses, for both crouch and stand cover. Fab lists UE 4.8–5.6. — [Fab: Cover Animset Pro](https://www.fab.com/listings/f9e9fbcb-0f07-49a0-8c06-80b18eba0e90)
- Kubold's 2020 blog says its packs include FBX sources that work in any engine and carry a perpetual commercial licence. That is older UE Marketplace-era wording; check the current Fab licence. — [kubold.com](https://www.kubold.com/)

Big Locomotion Pack V05 (Fab, 2026-07-22):
- 738 animations plus 36 aim-offset poses, combining unarmed, cover, M4A1, pistol, shotgun and sniper packs. — [Fab](https://www.fab.com/listings/83060727-3112-4d31-af3a-f35f6236f518?lang=en)

Silent Assassin Animation Bundle (Fab):
- The listing claims 380+ stealth animations, including stealth movement, takedowns from cover, corners, above and below, knife, gun and unarmed grabs, hostage handling and victim reactions.
- It is hand-keyed in Maya (not mocap), compatible with the UE5 Mannequin, and lists UE 5.8.
- Source: [Fab](https://www.fab.com/listings/b7f4345b-eadc-465c-8ad4-876c30322e39?lang=pl)

"Cover System" (Fab, 2025-12-15):
- 242 animation files (121 root-motion plus 121 in-place) and 17 Blueprints, on the UE5 Mannequin with IK bones, plus FBX and MotionBuilder sources.
- "Not currently tested for Multiplayer."
- Source: [Fab](https://www.fab.com/listings/bb06916a-cb63-41c5-aad7-b54b5b01a320)

Prices of these Fab packs were not captured. UNVERIFIED.

#### M. Rigging tools (Blender side)

Auto-Rig Pro (Artell):
- The Full version is $50 (Smart auto-rig, Remap retargeting, FBX/GLTF export). The Lite version is $25. It supports Blender 2.93–5.2 and is GPL. — [Superhive](https://superhivemarket.com/products/auto-rig-pro)
- Its UE export supports Mannequin bone hierarchy, axes, naming and IK bones for UE4 and UE5. It recommends a character height of about 1.80 m and requires the rig to be named "root".
  - Its docs say "6 spine bones and 2 neck bones are required to match the UE5 Mannequin".
  - Export is scriptable through Python (`arp_export`, `scn.arp_engine_type='UNREAL'`).
  - Source: [ARP Game Engine Export docs](https://www.lucky3d.fr/auto-rig-pro/doc/ge_export_doc.html)
- The Gumroad listing says yearly renewal is optional; the add-on works perpetually. — [Gumroad](https://artell.gumroad.com/l/autorigpro)

Epic BlenderTools (Send to Unreal, UE to Rigify):
- An independently maintained community fork targets Blender 5.x and UE 5.7. Validation is "in progress", and UE 5.5+ needs the legacy FBX importer (`Interchange.FeatureFlags.Import.FBX=False`). — [GitHub fork (Tzeentchnet)](https://github.com/Tzeentchnet/BlenderTools); original [EpicGames/BlenderTools](https://github.com/EpicGames/BlenderTools)

Rigify + Uefy:
- A community workflow generates Rigify rigs from Manny/Quinn metarig presets. Epic's Simple Manny/Quinn is exported, imported into Blender at unit scale 0.01, and the user's mesh is skinned to it. — [3D Artist's Handbook](https://sidney-eliot.github.io/3d-artists-handbook/presentation/blender-to-unreal-pipeline)

Epic staff guidance (Sept 2025):
- "We recommend following the Unreal Mannequin content approach."
- Mannequin arm chains follow Maya's X+ down the bone, while Blender uses Y+.
- Export the mannequin FBX and load it into Blender as an orientation reference.
- Use Control Rig for runtime procedural adjustments.
- Source: [Epic forum](https://forums.unrealengine.com/t/best-practices-for-creating-characters-in-blender-and-exporting-to-unreal/2662405)

### Inferences
- The free stack (UE 5.8, GASP and the MetaHuman markerless plugin) changes the economics for a solo developer. Unarmed locomotion and traversal, physical reactions, and quick self-captured one-off actions are now essentially free. Paid budget should go to genre-specific content (weapons, cover, stealth, takedowns) and to cleanup tooling.
- Do not depend on a single cloud AI-mocap vendor for multi-year production. RADiCAL closing and Kinetix being absorbed, both within 2026, show the risk. Keep source videos and exported FBX archived locally.
- Mover and UAF are Experimental. For a solo developer who must ship, the lower-risk path is CMC plus Anim Blueprint plus Motion Matching. GASP keeps both CMC-based and Mover-based characters (the 5.7 blog says a new character "that uses the Mover Plugin" was added). Treat Mover adoption as an explicit risk decision.

### Gaps
- Prices for individual ActorCore packs, iClone 8, Flow Studio tiers, DeepMotion tiers and the Fab packs did not render. UNVERIFIED.
- The maturity labels for Motion Warping, Full Body IK and Animation Deformers in 5.8 could not be confirmed because the docs returned 403.
- Animation Deformers were not researched in depth. The 5.8 release notes mention "Deformer Rigs" in the Animator Kit (bug fix only). — [Epic forum release thread](https://forums.unrealengine.com/t/unreal-engine-5-8-released/2729274)

---

## 2. Epic Game Animation Sample (GASP): contents, licence, UE version, commercial shipping

### Takeaway
GASP is free on Fab and supports UE 5.4–5.8. It is the best free AAA-grade locomotion foundation available: 500+ mocap animations (plus 400 added in 5.7), motion matching, traversal, smart-object sitting, and (in 5.8) multi-character takedowns and powered ragdoll.

Its Fab licence is "UE-Only Content", so its animations can ship in a commercial UE game, including a Steam release, but cannot be used in other engines or redistributed raw. It does not provide firearm, aiming, reload or cover sets, judging by Epic's descriptions.

### Cited Findings

What the Fab listing says:
- "3rd person capsule driven locomotion, powered by Motion Matching, complete with walk, run, jump and fall states"
- "A simple traversal system showcasing motion matched blend-ins and blend-outs"
- "500+ of game-ready animations compatible with the UE5 Mannequin Skeleton through runtime retargeting"
- Unreal Engine versions 5.4–5.8; development on Windows and macOS; ships as a complete project.
- Source: [Fab listing](https://www.fab.com/listings/880e319a-a59e-4ed2-b268-b32dac7fa016)

Licence:
- The Fab listing states "UE-Only Content - Licensed for Use Only with Unreal Engine-based Products". — [Fab listing (?lang=en)](https://www.fab.com/listings/880e319a-a59e-4ed2-b268-b32dac7fa016?lang=en)
- Epic's own launch blog says: "You're free to use all these animations with or without their accompanying animation Blueprints and retarget them to your own characters." It also says the animations are "compatible with all UE Mannequins" and can be used "for humanoid locomotion in your own projects". — [Epic blog: GASP launch](https://www.unrealengine.com/blog/game-animation-sample)
- A community reading of the UE-only licence (June 2024) says you may use it in a commercial game, edit it in Blender, and mix it with other packs, as long as the product is UE-based. This is not an Epic statement. — [Epic forum](https://forums.unrealengine.com/t/what-can-i-do-with-game-animation-sample-that-recently-was-at-100-off/1903980)

Data and systems:
- The project has multiple Motion Matching databases, with dense and sparse datasets.
- Selection runs mostly through Choosers and Proxy Tables, with Pose Warping covering gaps in animation coverage, and the Rewind Debugger for debugging.
- Epic says it works with MetaHumans.
- Source: [Epic blog: GASP launch](https://www.unrealengine.com/blog/game-animation-sample)

5.7 update (2025-12-03):
- A Mover-plugin character and a new locomotion dataset that balances responsiveness against quality, with "400 new animations".
- Simple Spring Walking and Smooth Walking movement modes.
- A Smart Object level, with NPCs sitting on benches "from multiple approach angles".
- An Experimental Pose Search Column in Choosers.
- A slope-aware slide.
- A Footplacement Control Rig node.
- Source: [Epic blog](https://www.unrealengine.com/en-US/tech-blog/explore-the-updates-to-the-game-animation-sample-project-in-ue-5-7)

5.8 update (2026-08-12):
- Physics Control Component and Physics Control Asset.
- Multi-character motion matching and Pose Search Interaction Assets.
- `SandboxCharacter_Mover_Ragdoll`: powered ragdoll with fall protection, injury, flail and roll states, plus motion-matched get-ups.
- "Shoves… tackles… and takedowns… Each interaction type includes coverage for all approach angles, and the takedowns include variations for standing, walking, and running."
- A RagdollLevel.
- An Experimental additive Look-At Control Rig solver, including a patrolling-NPC scan demo.
- A Chooser with an improved Pose Match Column, and slope animations using Blend Spaces.
- Players can now sit on benches through a State Tree.
- Source: [Epic blog](https://www.unrealengine.com/tech-blog/download-the-latest-game-animation-sample-project-now-updated-for-ue-5-8)

Version history:
- First released with UE 5.4; Epic updates it alongside major engine releases. — [GASP 5.7 blog](https://www.unrealengine.com/en-US/tech-blog/explore-the-updates-to-the-game-animation-sample-project-in-ue-5-7)

### Inferences
- **Licensing gate: PASS** for an Unreal Engine game sold on Steam. The binding text is "UE-Only"; Epic's blog explicitly permits use and retargeting in your own projects. Do not redistribute raw GASP files, for example in a mod kit or open-source repository outside UE. That is an inference from standard Fab terms; the licensing specialist should confirm against the full Fab EULA.
- GASP's 5.8 takedowns are physics- and ragdoll-oriented (shoves, tackles). They are not Hitman-style stealth chokeholds or hand-to-hand finishers. They are a technical template for the Pose Search Interaction Asset pipeline, into which bought or captured paired takedown animations can be loaded.
- The UE-version requirement is UE 5.4 or later. To get the latest features (interactions, ragdoll, Look-At), use UE 5.8.

### Gaps
- Exact per-category clip counts (for example, how many crouch or strafe clips) could not be confirmed because the GASP docs page returned 403.
- Whether GASP includes crouch locomotion: UNVERIFIED.
- Whether the planned UAF-only character shipped in the 5.8 GASP: UNVERIFIED. The 5.8 blog does not mention it.
- Fab EULA "UE-Only" full legal text not fetched (licensing specialist overlap).

---

## 3. Mixamo in 2026: prototypes vs professional quality

### Takeaway
Mixamo is still online, free and royalty-free for games in 2026, but unmaintained. It is fine for greyboxing and prototypes, and can even ship individual clips (only raw-file redistribution and ML training are banned).

It is not sufficient for AAA-feeling third-person animation:
- The library is old and inconsistent.
- It has no dense, pose-matched coverage of starts, stops, pivots and turns for motion matching.
- Its weapon, cover and stealth sets are limited (inference).
- There is no support or roadmap.

Professional quality needs GASP-style dense locomotion datasets, genre packs, or custom capture, plus warping and IK cleanup.

### Cited Findings
- Licence terms:
  - Allowed: free, unlimited commercial use, including video games and DLC.
  - Not allowed: redistributing raw character or animation files, or using them to train ML models.
  - No attribution required, and the terms in force at the time of use apply.
  - Source: [Adobe Community licensing FAQ](https://community.adobe.com/questions-696/mixamo-faq-licensing-royalties-ownership-eula-and-tos-589400)
- The help page was last updated in 2021. — [Adobe helpx](https://helpx.adobe.com/creative-cloud/faq/mixamo-faq.html)
- As of Sept 2026 it works and is free, with no significant updates since 2015. There was an outage in June 2025, and an unofficial claim that it is "not supported anymore". — [Cinevva (vendor blog)](https://app.cinevva.com/guides/free-character-animations-rigging); [Adobe community](https://community.adobe.com/questions-696/mixamo-not-working-589817)
- Mixamo-named rigs (LeftArm, LeftForeArm…) auto-map well in the IK Retargeter's fuzzy chain matching. — [Cinevva export checklist, 2026-09-06 (vendor blog)](https://app.cinevva.com/guides/blender-to-unreal-export-checklist)
- Free alternatives that are actively developed: AccuRIG 2 (free, UE export preset) — [Reallusion magazine, 2025-07-30](https://magazine.reallusion.com/2025/07/30/accurig-2-vs-mixamo-smarter-auto-rigging-for-3d-animators/) — and Quaternius UAL1/UAL2 (CC0) — [Quaternius](https://quaternius.com/packs/universalanimationlibrary2.html).

What professional quality needs, per Epic:
- Motion matching quality "scales to the size of your dataset", with dense datasets of hundreds of animations versus sparse sets. — [Epic GASP blog](https://www.unrealengine.com/blog/game-animation-sample)
- Play-rate and stride-warp corrections beyond 15–20% visibly degrade quality. Animations must therefore be authored close to gameplay speeds. — [Epic Lyra blog (2022)](https://www.unrealengine.com/tech-blog/adapting-lyra-animation-to-your-ue5-game)

### Inferences
- Use Mixamo or Quaternius for blocking and AI-agent prototyping (for example, quick NPC idles).
- Replace hero-character locomotion with GASP data (unarmed) and paid armed sets (MoCap Online and others).
- Keep Mixamo clips only for background NPCs or one-off actions after cleanup.
- Archive every downloaded Mixamo file, because the service could disappear without notice.

### Gaps
- No official Adobe end-of-life notice or roadmap was found.
- Mixamo's current animation count was not verified.

---

## 4. Blender character to UE animation-ready: step-by-step pipeline

### Takeaway
The most robust path for a custom Blender character is to make its skeleton UE5-Mannequin-compliant in Blender: same names, hierarchy, root then pelvis, and Mannequin axes. Auto-Rig Pro ($50) does this out of the box; Rigify+Uefy or skinning to Epic's exported Manny skeleton are cheaper routes. Then export a clean FBX, import it into UE 5.8, and set up an IK Rig and IK Retargeter, scripted where possible.

This makes GASP, Fab packs (mostly UE4/UE5 Mannequin), Mixamo, ActorCore and markerless captures all retargetable with minimal per-pack work.

### Cited Findings
- Epic staff recommend following the Mannequin content approach and loading the exported Mannequin FBX into Blender as a reference. Axis conventions differ: X+ down the bone (Maya and Epic) versus Y+ (Blender). — [Epic forum, 2025-09-03](https://forums.unrealengine.com/t/best-practices-for-creating-characters-in-blender-and-exporting-to-unreal/2662405)
- Auto-Rig Pro export:
  - Humanoid mode supports UE Mannequin naming, axes and IK bones.
  - The rig must be named `root`, and the character should be about 1.80 m tall.
  - UE5 Mannequin matching needs "6 spine bones and 2 neck bones".
  - "In latest UE versions, animation retargeting does not really depend anymore on the pose."
  - For a Control Rig, you can duplicate the default UE Control Rig for any humanoid exported with UE Humanoid settings.
  - Source: [ARP docs](https://www.lucky3d.fr/auto-rig-pro/doc/ge_export_doc.html)
- FBX export settings (vendor guide, Sept 2026):
  - Selected Objects; Apply Unit; Apply Scalings "FBX All" (or scene unit scale 0.01).
  - Smoothing set to Face; Add Leaf Bones off; Primary Bone Axis Y, Secondary X.
  - Only Deform Bones for Rigify.
  - Rename the armature object to `root` so UE does not add an extra "Armature" bone.
  - Source: [Cinevva checklist](https://app.cinevva.com/guides/blender-to-unreal-export-checklist)
- Rigify/Uefy route:
  - Set unit scale 0.01, import SKM_Manny/Quinn_Simple with Automatic Bone Orientation, and add a Uefy Manny/Quinn metarig.
  - Generate the rig and parent with Armature Deform.
  - Export with Only Deform Bones, no Bake Animations, no Leaf Bones, Smoothing Face.
  - In UE, import the skeletal mesh without animations and do not select an existing skeleton for the first character of a new skeleton type.
  - Then build an IK Rig: set the retarget root on the pelvis, add an FBIK solver, add hand and foot goals, and create retarget chains.
  - Source: [3D Artist's Handbook](https://sidney-eliot.github.io/3d-artists-handbook/presentation/blender-to-unreal-pipeline)
- IK Retargeter ops and scripting: [Python IKRetargeterController](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/IKRetargeterController); batch retargeting with override sets: [IKRetargetBatchOperationInputs](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/IKRetargetBatchOperationInputs). Auto-generating a retargeter only works for known skeleton types: [BatchRetargetSettings](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/BatchRetargetSettings).
- UE 5.8 adds foot definitions for retargeting and Retarget Override Sets for differing proportions. — [Epic 5.8 news](https://www.unrealengine.com/news/unreal-engine-5-8-is-now-available)
- AccuRIG 2 is a free auto-rig with an Unreal FBX target preset. Without Character Creator there is no skin-weight editing. — [AccuRIG manual](https://manual.reallusion.com/AccuRig-2/2.0/09-add-motions/export.htm); [ActorCore AccuRIG](https://actorcore.reallusion.com/auto-rig)
- Send to Unreal (community fork) pushes meshes and animations from Blender to a running UE editor. It needs the Python Editor Script Plugin with remote execution, and the legacy FBX importer on UE 5.5+. — [GitHub fork](https://github.com/Tzeentchnet/BlenderTools)
- MoCap Online rifle animations attach the weapon to the `middle_01_r` finger bone (socket), with `ik_hand_gun` matching it. Weapon attachment conventions therefore must survive your rig. — [Fab: Rifle Shooter Pro](https://www.fab.com/listings/6cbfcfb5-e96b-4a69-b8f2-124844c17b8f); [mocaponline.com](https://mocaponline.com/products/rifle)

### Step-by-step pipeline (synthesised; each step traces to the sources above)

1. **Pick a skeleton strategy (decide once, early).**
   - Option A (recommended): a UE5-Manny-compliant skeleton. Use the same bone names and hierarchy, including `root`, `pelvis`, `spine_01`–`05`, `neck_01`–`02`, twist bones and `ik_*` bones, so retargeting from GASP and Fab packs is trivial.
   - Option B: a custom skeleton, accepting more IK Retargeter setup per source.
2. **Reference.** Export `SKM_Manny` (or Quinn) from UE as FBX and import it into Blender with unit scale 0.01 to match orientations, proportions and the A-pose.
3. **Rig.** Choose one of:
   - (a) Auto-Rig Pro Full ($50): Smart auto-rig, then Humanoid export with UE Mannequin naming, axes and IK bones. The rig object must be named `root`.
   - (b) Rigify plus a Uefy Manny metarig (Uefy lite is free; pro is paid, price UNVERIFIED).
   - (c) Skin the mesh directly to the imported Manny skeleton.
   - (d) AccuRIG 2 or the Mixamo auto-rigger for speed. These give non-Manny names and need an IK Retargeter, and offer less control over twist bones.
4. **Skinning QA.** Make sure every skinned bone has Deform enabled, and add twist and corrective bones. Plan runtime correctives with Control Rig or a Post Process AnimBP rather than baked-only (Epic staff advice).
5. **Export FBX.** Selected Objects; Apply Unit plus FBX All scalings (or unit scale 0.01); Smoothing Face; Add Leaf Bones off; Only Deform Bones; Primary Y, Secondary X (or let ARP's exporter handle axes). Turn Bake Animation off for the mesh export.
6. **Import into UE 5.8** as a Skeletal Mesh without animations.
   - New skeleton for a new rig type. Reuse the Mannequin skeleton only if the hierarchy is truly identical (Option A).
   - Create the Physics Asset. Add weapon sockets that match your animation packs' attach bone, for example `middle_01_r` or `hand_r`.
7. **IK Rig.** Set pelvis as the retarget root, auto-create retarget chains, and add FBIK hand and foot goals.
8. **IK Retargeter.**
   - Source is the UE5 Manny IK Rig (GASP, Fab), or the MetaHuman, Mixamo or ActorCore rigs.
   - Use the default op stack (Pelvis Motion, FK Chains, IK Chains, IK Solve, Root Motion).
   - Auto-map chains and fix any mismaps.
   - Align the retarget pose (A versus T; auto-align).
   - Use 5.8 foot definitions, and override sets for proportion variants.
9. **Retarget mode.**
   - Offline batch retarget (`run_batch_retarget`) creates new sequences on your skeleton. This is best for editing and for Pose Search databases.
   - Alternatively, use runtime retargeting from Manny, which is how GASP feeds non-Manny meshes.
10. **Post-retarget prep.**
    - Add distance curves (Distance Curve modifier, which can be bulk-applied).
    - Mark root motion and in-place versions.
    - Build Pose Search databases and Choosers.
    - Duplicate the default Control Rig for in-editor fixes and a foot-placement or Look-At layer.
11. **Validate.** Use the Rewind Debugger and test on slopes and stairs. Check foot contact, hand-on-weapon IK, and penetration (fix in Control Rig or Cascadeur).

### Inferences
- Option A plus Auto-Rig Pro is the lowest-friction choice for a solo developer whose AI agent will script the steps. ARP's export is Python-callable, and the UE retarget and batch APIs are Python-callable.
- If the hero character will be a MetaHuman (character specialist), body animation retargets through the MetaHuman skeleton instead. Same pipeline, different source IK Rig.

### Gaps
- Exact Blender FBX "Forward" axis guidance for UE was not verified from a primary source.
- The current maintenance status of Epic's original BlenderTools repo (archived or not) was not verified; only the community fork was seen.

---

## 5. Retargeting artifacts, foot sliding, blending, contacts: practical fixes

### Takeaway
Most "amateur" tells are foot sliding, floaty feet, pose drift after retargeting, hand-weapon misalignment and interpenetration. Each is fixed by a layered approach:
- Author or buy animations near gameplay speeds.
- Distance-match transitions, with play-rate clamped to about 0.75–1.25.
- Stride- and orientation-warp within about 15–20%.
- Plant feet with Foot Placement or Control Rig.
- Align contextual actions with Motion Warping.
- Clean raw or AI mocap in Cascadeur (collision penetration, AutoPhysics) or Flow Studio (foot locking).

### Cited Findings
- Distance matching, with a play-rate clamp defaulting to [0.75, 1.25] and stride warping to "make up the difference". — [Python AnimDistanceMatchingLibrary](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/AnimDistanceMatchingLibrary?application_version=5.7)
- Stride warping includes FK-limit clamping and a pelvis pull-down solver. — [Python AnimNode_StrideWarping](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/AnimNode_StrideWarping?application_version=5.4)
- Orientation warping provides directional strafing coverage with spine counter-rotation. — [Python AnimNode_OrientationWarping](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/AnimNode_OrientationWarping?application_version=5.7)
- 15–20% is the maximum correction before quality drops; sometimes accept sliding, or reduce gameplay speed or acceleration; use the turn-in-place root yaw offset. — [Epic Lyra blog (2022)](https://www.unrealengine.com/tech-blog/adapting-lyra-animation-to-your-ue5-game)
- Pose Warping fills gaps in motion-matching coverage. — [Epic GASP blog](https://www.unrealengine.com/blog/game-animation-sample)
- The Footplacement Control Rig node (5.7) and the additive Look-At Control Rig (5.8, Experimental). — [GASP 5.7](https://www.unrealengine.com/en-US/tech-blog/explore-the-updates-to-the-game-animation-sample-project-in-ue-5-7); [GASP 5.8](https://www.unrealengine.com/tech-blog/download-the-latest-game-animation-sample-project-now-updated-for-ue-5-8)
- Retarget fixes: `snap_bone_to_ground`, IK chain goals, retarget pose offsets, root settings and speed-plant settings. — [IKRetargeterController](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/IKRetargeterController); [IKRetargeter](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/IKRetargeter). 5.8 adds foot definitions and Retarget Override Sets for differing proportions. — [Epic 5.8 news](https://www.unrealengine.com/news/unreal-engine-5-8-is-now-available)
- Motion Warping targets can follow moving components or bones, which suits alignment to enemies, cover edges and vehicle doors. — [UE 5.8 BP API](https://dev.epicgames.com/documentation/en-us/unreal-engine/BlueprintAPI/MotionWarping/AddorUpdateWarpTargetfromCompone-)
- Multi-character interactions use Pose Search Interaction Assets with "tooling for easy motion warp handling". — [GASP 5.8](https://www.unrealengine.com/tech-blog/download-the-latest-game-animation-sample-project-now-updated-for-ue-5-8)
- Cascadeur cleanup:
  - Collision Penetration Cleaning removes self- and ground-interpenetration.
  - AutoPhysics turns keyed or mocap animation into a physically plausible version.
  - Animation "unbaking" for mocap editing.
  - Inbetweening and Motion Generation.
  - Sources: [CG Channel](https://www.cgchannel.com/2026/04/nekki-releases-cascadeur-2026-1/); [Cascadeur plans](https://cascadeur.com/plans); [2026.2 blog](https://cascadeur.com/blog/view/cascadeur-2026-2-animation-layers-easing-new-languages)
- AI-mocap cleanup in Flow Studio: automatic foot locking, smoothing and trajectory fixes. — [Autodesk](https://www.autodesk.com/solutions/media-entertainment/ai-motion-capture-with-flow-studio)
- AI mocap failure modes as reported:
  - Floating feet and degraded quality with moving cameras (MetaHuman markerless). — [CG Channel](https://www.cgchannel.com/2026/06/get-the-free-metahuman-animator-markerless-mocap-plugin/)
  - Best results when the subject stays grounded. — [CGDive](https://addons.cgdive.com/tools/unreal-engine-markerless-mocap)
  - Root drift on inertial suits, which Rokoko's Coil Pro addresses. — [Rokoko Coil Pro](https://www.rokoko.com/products/coil-pro)
- MotionMaker's team built a foot-slide reduction tool because foot sliding was "a common concern". — [Autodesk blog](https://blogs.autodesk.com/media-and-entertainment/2025/06/04/meet-motionmaker/)

### Inferences
- **Critical view of AI mocap:** none of the AI or markerless vendors publish accuracy metrics. Treat output as blocking or "70% data" that needs foot-contact repair, hand and finger review, and prop-contact fixes before it ships on a hero character. This is especially true for fast combat, ground work (crawl, takedowns) and two-person contact, where occlusion is worst.
- Prop or weapon contact (reloads, door handles, steering wheels) is the weakest area for every markerless solution. Plan for Control Rig IK passes in Sequencer, or Cascadeur constraints. Cascadeur 2026.1 notes that constraints are now respected by AutoPosing, AutoPhysics and Ragdoll.

### Gaps
- No published side-by-side benchmark of Move AI, MetaHuman markerless, Rokoko Vision, DeepMotion and Flow Studio accuracy was found.

---

## 6. Mocap hardware and costs (what a solo developer actually needs)

### Takeaway
There are three price tiers:
- **About $0.** Webcam or phone with the free MetaHuman markerless plugin. Needs a capable Windows PC (RTX 3070-class with 8 GB VRAM and 32 GB RAM recommended) and patience: about 15–30 minutes of processing per 30-second clip.
- **About $15–$250 per month.** Cloud single- or multi-camera AI: Move AI, Rokoko Vision, Flow Studio, DeepMotion.
- **About $2.3k–$7.8k one-off.** Rokoko inertial suit, gloves and Coil Pro. This removes occlusion problems and gives the most repeatable data for a solo performer.

### Cited Findings
- MetaHuman markerless: a webcam or phone, with Live Link Face (iOS) set to a non-TrueDepth camera. Windows only, DX12, 16 cores, 32 GB RAM, RTX 3070 / RX 6800 XT with 8 GB VRAM recommended. — [Digital Production](https://digitalproduction.com/2026/06/23/metahuman-gets-markerless-mocap/); [CGDive](https://addons.cgdive.com/tools/unreal-engine-markerless-mocap)
- Move One uses an iPhone or web upload at 1080p/60 fps, single camera. Move Pro uses multiple standard cameras (the trial allows up to 4). Move Live uses 4–8 cameras. — [Move AI docs](https://docs.move.ai/knowledge/move-ai-pricing-plans-credits); [Move AI pricing](https://www.move.ai/pricing)
- Rokoko hardware prices: Smartsuit Pro II (Glove Ready) $2,295; Coil Pro $2,995; Gloves + Coil bundle $4,795; Full Performance bundle $4,495; indie discount up to 30% if eligible. — [Rokoko store](https://store.rokoko.com/products/smartsuit-pro-ii-glove-ready); [Coil Pro](https://store.rokoko.com/products/coil-pro); [bundle](https://www.rokoko.com/products/full-performance-capture); [indie](https://www.rokoko.com/mocap/indie-creator-bundle)
- iClone Motion LIVE supports Xsens, Perception Neuron, Vicon, OptiTrack and Rokoko for live retargeting. — [Reallusion perpetual page](https://www.reallusion.com/plan-and-pricing/individual/perpetual)

### Inferences
- For an espionage game, a single performer needs a clear space of at least roughly 5 m x 5 m (Move One's stated volume), plus props: replica pistol and rifle, a chair, a car-door mock-up. Cover and vehicle entry are spatially constrained actions, which favours inertial suits or multi-camera setups over single-camera capture.
- Recommended order: start with the free MetaHuman plugin. Buy a Rokoko suit only if monthly capture volume justifies it, after the vertical slice.

### Gaps
- Xsens and Perception Neuron hardware prices were not researched (out of budget).
- A camera list for Move Pro was not verified.

---

## 7. Automation: what an AI coding agent (Claude Code) can do vs manual work

### Takeaway
An AI agent can script most of the pipeline mechanics:
- Blender rig export, through Auto-Rig Pro's Python export or bpy FBX.
- UE batch import (overlap with other specialists).
- IK Rig and Retargeter creation, chain mapping and batch retargeting, through the Python `IKRetargeterController` and `IKRetargetBatchOperation` APIs.
- Animation-modifier passes, MetaHuman Animator processing (Python and Blueprints), and vendor APIs: Move AI, DeepMotion, Uthana GraphQL, Rokoko command API, Cascadeur Python.
- The experimental UE 5.8 MCP plugin can expose editor operations to an LLM.

Human judgement is still needed for:
- Skin weights and deformation QA.
- Retarget-pose alignment sign-off.
- Performing the mocap.
- Motion-feel tuning: motion-matching weights, play-rate and warp limits.
- Contact cleanup and final acceptance.

### Cited Findings
- UE Python APIs (documented as "Unreal Python 5.8 (Experimental)"):
  - `IKRetargeterController`: add ops, auto-map chains, set source chains, retarget pose. — [link](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/IKRetargeterController)
  - `IKRetargetBatchOperation.run_batch_retarget`. — [link](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/IKRetargetBatchOperation)
  - `BatchRetargetSettings.auto_generate_retargeter`. — [link](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/BatchRetargetSettings)
  - `AnimDistanceMatchingLibrary`. — [link](https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/AnimDistanceMatchingLibrary?application_version=5.7)
- The MetaHuman Animator markerless plugin offers "automation available through Python and Blueprints". A forum user also notes that video-to-image-sequence conversion can be done with "Handbrake or Claude Code". — [Epic forum](https://forums.unrealengine.com/t/epic-games-metahuman-animator-markerless-motion-capture-plugin/2729293)
- The UE 5.8 Experimental MCP plugin "enables LLM systems—you can use any model you want—to connect to and understand both the engine and your project". — [Epic 5.8 news](https://www.unrealengine.com/news/unreal-engine-5-8-is-now-available)
- Auto-Rig Pro's export is scriptable (`arp_export`, scene properties). — [ARP docs](https://www.lucky3d.fr/auto-rig-pro/doc/ge_export_doc.html)
- Send to Unreal drives an open UE editor over Python remote execution. — [GitHub fork](https://github.com/Tzeentchnet/BlenderTools)
- Vendor APIs:
  - Move AI: API on Enterprise and developers.move.ai. — [Move AI docs](https://docs.move.ai/knowledge/move-ai-pricing-plans-credits)
  - DeepMotion: API plans. — [DeepMotion FAQ](https://www.deepmotion.com/pricing-animate3d)
  - Uthana: GraphQL API. — [Uthana FAQ](https://uthana.com/faq)
  - Rokoko: "Advanced command API" on Pro. — [Rokoko pricing](https://www.rokoko.com/pricing)
  - Cascadeur: Python API improved in 2026.2. — [Cascadeur 2026.2](https://cascadeur.com/blog/view/cascadeur-2026-2-animation-layers-easing-new-languages)
- Lyra workflow: distance curves can be bulk-added through Animation Modifiers from the Content Browser. — [Epic Lyra blog](https://www.unrealengine.com/tech-blog/adapting-lyra-animation-to-your-ue5-game)

### Inferences

| Step | Automatable by agent | Manual |
|---|---|---|
| Blender rig generation | Partial (ARP Smart needs guide placement) | Guide-marker review, weight painting |
| FBX export, naming, scale | Yes (bpy / ARP Python) | Spot-check |
| UE import and asset organisation | Yes (UE Python; overlap) | — |
| IK Rig chains, retargeter creation | Yes (controllers) | Retarget-pose visual sign-off |
| Batch retarget of hundreds of clips | Yes (`run_batch_retarget`) | Sample QA |
| Distance curves, root-motion flags | Yes (modifiers; exact Python call UNVERIFIED) | — |
| Pose Search DB build and tuning | Partial (asset creation UNVERIFIED via Python) | Weight tuning, feel |
| Video-to-mocap jobs | Yes (MHA Python/BP; Move/DeepMotion/Uthana APIs) | Performing, shot planning |
| Cleanup (foot contacts, props, penetration) | Low (Cascadeur Python scope UNVERIFIED) | Animator judgement |
| Licence tracking spreadsheet | Yes | Legal reading |

### Gaps
- Whether Pose Search databases and Choosers can be fully authored through Python in 5.8 was not verified.
- The scope of Cascadeur's Python API (batch AutoPhysics?) was not verified.

---

## 8. Coverage matrix: animation need → best free source / best paid source / gaps

### Takeaway
Free sources fully cover unarmed locomotion, traversal, sitting, physical reactions and basic interactions (GASP plus the markerless plugin). Paid Fab packs cover weapons, cover and stealth takedowns. Vehicle entry and exit, ladder and pipe climbing, and original-weapon reloads are the main gaps. They need custom capture (MetaHuman markerless, Rokoko, Move AI) plus Motion Warping.

### Cited Findings (matrix; sources are in Sections 1–2)

| Animation need | Best free source | Best paid source | Gaps / notes |
|---|---|---|---|
| Walk/run/sprint/turns/starts/stops | GASP motion-matched locomotion, 500+ anims plus 400 in 5.7 ([Fab](https://www.fab.com/listings/880e319a-a59e-4ed2-b268-b32dac7fa016), [5.7 blog](https://www.unrealengine.com/en-US/tech-blog/explore-the-updates-to-the-game-animation-sample-project-in-ue-5-7)); Quaternius UAL (CC0) | Big Locomotion Pack V05, unarmed part ([Fab](https://www.fab.com/listings/83060727-3112-4d31-af3a-f35f6236f518?lang=en)) | Distinct hero gait or style needs custom capture |
| Combat locomotion (armed strafing) | Quaternius UAL "gun" (limited); Mixamo (limited) | MoCap Online Rifle Pro 390+ / Pistol Pro 370+ ([Fab](https://www.fab.com/listings/6cbfcfb5-e96b-4a69-b8f2-124844c17b8f)); Big Locomotion Pack (M4A1, pistol, shotgun, sniper) | No free motion-matching-dense armed dataset found |
| Aiming and shooting | UE Aim Offsets plus Control Rig (engine) | MoCap Online aim offsets; Big Locomotion (36 AO poses); Kubold Cover (43 AO poses) | Recoil is usually procedural (not researched here) |
| Weapon handling and reloads | Mixamo (generic) | MoCap Online reloads, swaps, holsters | Original weapon designs need bespoke reloads (capture with prop or keyframe in Cascadeur) |
| Cover transitions | None adequate found | Cover System (242 files) ([Fab](https://www.fab.com/listings/bb06916a-cb63-41c5-aad7-b54b5b01a320)); Kubold Cover Animset Pro ([Fab](https://www.fab.com/listings/f9e9fbcb-0f07-49a0-8c06-80b18eba0e90)); Big Locomotion cover pack | Pistol/unarmed cover variety; corner swaps need Motion Warping |
| Stealth movement | Mixamo crouch walks; UAL crawl | Silent Assassin bundle (stealth locomotion, ledge hang, dragging) ([Fab](https://www.fab.com/listings/b7f4345b-eadc-465c-8ad4-876c30322e39?lang=pl)) | Silent Assassin is hand-keyed, not mocap, so style may need matching |
| Crouch / crawl / climb / vault | GASP traversal (motion-matched blend-ins/outs); UAL crawl | Locomotion packs (crouch variants) | Ladders, pipes, ledge shimmy: no pack verified (gap) |
| Melee takedowns | GASP 5.8 multi-character shoves, tackles, takedowns, all approach angles ([5.8 blog](https://www.unrealengine.com/tech-blog/download-the-latest-game-animation-sample-project-now-updated-for-ue-5-8)) | Silent Assassin bundle (takedowns, grabs, hostages, victim reactions) | Paired anims must be fitted into Pose Search Interaction Assets |
| Vehicle enter/exit | None found | None verified | **Gap.** Capture per vehicle class and align with Motion Warping (follow-component targets); overlaps with the vehicles specialist |
| Sitting and environment interactions | GASP Smart Object benches (NPC plus player via State Tree) | ActorCore prop-paired "MotionPlus" and interaction packs ([ActorCore](https://actorcore.reallusion.com/3d-motion-technology?a=)) | Doors, keypads, laptops: capture or ActorCore |
| Conversations and facial performance | MetaHuman Animator face (character specialist); markerless body plus face from one camera | ActorCore conversation/interpersonal packs | Lip-sync and face are owned by the character specialist |
| Cinematic movement | MetaHuman markerless plugin (free); Control Rig in Sequencer | Rokoko suit; Move AI multi-cam; Cascadeur | Multi-actor cinematic capture: Move Pro (up to 8 people) or multiple suits |
| Root motion | All packs ship root-motion plus in-place; Motion Warping | — | Mover root-motion integration still maturing (Experimental) |
| Motion matching | Pose Search plus GASP databases | Denser commercial datasets ("Motion Matching-ready" packs: none verified) | Building dense armed databases is the main cost |
| Procedural animation | Control Rig, FBIK, Foot Placement, Look-At (Experimental), Physics Control, Control Rig Dynamics (5.8) | — | UAF and Locomotor are Experimental |
| IK | IK Rig / FBIK / Control Rig | — | — |
| Retargeting between skeletons | IK Retargeter (op stack, 5.8 foot definitions, override sets); runtime retargeting | Cascadeur Pro retargeting; ARP Remap ($50) | Non-humanoid or stylised: more manual work |

### Inferences
- The budget focus should be:
  1. An armed locomotion set from one vendor, for consistent style.
  2. A cover set.
  3. A stealth and takedown set.
  4. Custom capture days for vehicle entry and exit and signature actions.

### Gaps
- Prices for all Fab packs: UNVERIFIED.
- No commercial "motion-matching-ready" armed dataset was verified.
- No vehicle-entry pack was searched for specifically; this was beyond the search budget.

---

## 9. Category verdicts

### Takeaway
- Best overall: UE 5.8's built-in systems plus GASP.
- Best free: GASP plus the MetaHuman markerless plugin plus Quaternius CC0.
- Best paid: genre packs on Fab plus Cascadeur.
- Best AI-assisted: Cascadeur for cleanup and authoring; the MetaHuman plugin for capture.
- Best for an indie developer: the free stack plus Cascadeur Indie plus targeted Fab packs.
- Avoid: RADiCAL and Kinetix (gone), the non-commercial free tiers, and Mover/UAF for shipping without accepting the Experimental risk.

### Cited Findings (verdict rationale; sources in Sections 1–7)

| Category | Verdict | Reason |
|---|---|---|
| **Best overall** | UE 5.8 built-ins (Motion Matching/Pose Search, IK Retargeter, Control Rig, warping, Motion Warping) plus GASP | Free; Epic-maintained; motion matching and multi-character interactions; UE-Only licence passes a Steam release ([Fab](https://www.fab.com/listings/880e319a-a59e-4ed2-b268-b32dac7fa016), [5.8 blog](https://www.unrealengine.com/tech-blog/download-the-latest-game-animation-sample-project-now-updated-for-ue-5-8)) |
| **Best free** | GASP; MetaHuman Animator Markerless plugin; Quaternius UAL1/2 (CC0); AccuRIG 2; Mixamo (prototype) | All commercially usable; the MetaHuman plugin is "free, including for use on commercial projects" ([CG Channel](https://www.cgchannel.com/2026/06/get-the-free-metahuman-animator-markerless-mocap-plugin/)) |
| **Best paid** | Fab genre packs (MoCap Online Rifle/Pistol Pro; cover sets; Silent Assassin) plus Cascadeur Pro/Indie | Covers the espionage-specific gaps; Cascadeur gives AutoPhysics, penetration cleanup, UE Live Link ([Cascadeur](https://cascadeur.com/plans)) |
| **Best AI-assisted** | Cascadeur (AutoPosing, Inbetweening, Motion Generation, AutoPhysics); MetaHuman markerless (capture); Move AI (multi-cam, up to 8 people) | Cascadeur keeps the animator in control; the MetaHuman plugin is local and free; Move Pro covers multi-person |
| **Best for indie** | GASP + MetaHuman plugin + Cascadeur Indie ($8/month annual, under $100k revenue) + 2–3 Fab packs; Rokoko suit only when capture volume justifies $2.3k–$4.5k | Low cash burn; licensing passes |
| **Not recommended** | RADiCAL (shut down 2026-07-06); Kinetix (team joined Runway 2026-09-08); Cascadeur Free, DeepMotion Freemium and Move AI Personal for shipped content (non-commercial); Mixamo as the core hero-locomotion source (dated, unmaintained); Mover/UAF as a shipping foundation unless you accept Experimental API churn | See [CG Channel](https://www.cgchannel.com/2026/04/autodesk-acquires-core-tech-of-ai-motion-capture-firm-radical/), [Runway](https://runway.com/news/company-news/kinetix-joins-runway), [Cascadeur](https://cascadeur.com/plans), [DeepMotion](https://www.deepmotion.com/pricing-animate3d), [Move AI docs](https://docs.move.ai/knowledge/move-ai-pricing-plans-credits), [Mover docs](https://dev.epicgames.com/documentation/unreal-engine/mover-in-unreal-engine) |

### Inferences
- **Licensing gate summary for a commercial Steam release:**

| Source | Result |
|---|---|
| GASP | PASS (UE game) |
| MetaHuman markerless plugin | PASS |
| Mixamo | PASS (no raw redistribution; no ML training) |
| Quaternius | PASS (CC0) |
| ActorCore | PASS with the Standard/export licence; iContent is not enough |
| Fab packs | PASS under Fab Standard License (check each listing; UNVERIFIED per pack) |
| Cascadeur Indie | PASS if revenue is under $100k per year (exceeding it requires Pro) |
| Cascadeur Free | FAIL |
| DeepMotion | Premium PASS; Freemium FAIL |
| Move AI | Standard/Advanced PASS; Personal FAIL; current Starter/Basic UNVERIFIED |
| Flow Studio | Paid tiers needed |
| Rokoko Motion Library | PASS (commercial allowed) |
| Uthana | UNVERIFIED |

### Gaps
- The Fab Standard License and UE-Only full texts were not read (licensing specialist).

---

## 10. Tool database rows

### Takeaway
Rows for the master tool database. Unknown fields are marked UNVERIFIED. All sources were accessed 2026-10-09.

### Cited Findings

| Name | Category | Developer | URL | Purpose | Quality | Pricing | License | UE compatibility | Formats | AI integration (API/MCP/scripting/none) | Advantages | Limitations | Hardware | Recommendation | Confidence | Sources (URL + access date) |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| Game Animation Sample Project (GASP) | Free anim library + reference systems | Epic Games | https://www.fab.com/listings/880e319a-a59e-4ed2-b268-b32dac7fa016 | Motion-matched locomotion, traversal, interactions, takedowns, ragdoll | High (Epic-marketed as "AAA-quality" mocap) | Free | UE-Only Content (Fab) | UE 5.4–5.8 | UE project (uassets) | Scripting (UE Python/BP); none native | 500+ anims (+400 in 5.7); motion matching; multi-char interactions (5.8) | No weapon/cover sets (per Epic descriptions); UE-only | UE-capable PC | Essential | High | Fab listing; https://www.unrealengine.com/tech-blog/download-the-latest-game-animation-sample-project-now-updated-for-ue-5-8 ; https://www.unrealengine.com/blog/game-animation-sample (2026-10-09) |
| Motion Matching / Pose Search | Engine system | Epic Games | https://www.unrealengine.com/blog/game-animation-sample | Runtime pose selection from databases | High | Free (engine) | UE EULA | UE 5.4+; multi-char in 5.8 | Engine assets | Scripting (UE Python, partial UNVERIFIED) | Natural transitions; Choosers + Pose Search columns | Needs dense data; tuning effort | — | Essential | High | Epic blogs above (2026-10-09) |
| IK Rig / IK Retargeter | Engine retargeting | Epic Games | https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/IKRetargeterController | Retarget between skeletons, batch | High | Free | UE EULA | UE 5.x; op stack 5.6+; foot definitions + override sets in 5.8 | Engine assets | Python API (controllers, batch ops) | Fully scriptable; auto-generate for known skeletons | Custom skeletons need manual setup | — | Essential | High | Python API pages; https://www.unrealengine.com/news/unreal-engine-5-8-is-now-available (2026-10-09) |
| Control Rig (+ FBIK, Physics, Dynamics, Foot Placement, Look-At) | Engine rigging/procedural | Epic Games | https://www.unrealengine.com/news/unreal-engine-5-8-is-now-available | In-engine rigging, procedural layers, cleanup | High | Free | UE EULA | 5.8: Physics Beta; Dynamics new; Look-At Experimental | Engine | Scripting (UE Python, scope UNVERIFIED) | Runtime procedural; Sequencer keying; Zebra sample rig | Learning curve; some parts Experimental | — | Essential | High | Epic 5.8 news; GASP 5.7/5.8 blogs (2026-10-09) |
| Animation Warping (Distance Matching, Stride, Orientation) | Engine anim nodes | Epic Games | https://dev.epicgames.com/documentation/en-us/unreal-engine/python-api/class/AnimDistanceMatchingLibrary?application_version=5.7 | Fix foot sliding and speed/direction mismatch | High within about 15–20% | Free | UE EULA | UE 5.x | — | Python library exposure | Proven in Lyra/GASP | Over-warping degrades poses | — | Essential | High | Python API; https://www.unrealengine.com/tech-blog/adapting-lyra-animation-to-your-ue5-game (2026-10-09) |
| Motion Warping | Engine root-motion alignment | Epic Games | https://dev.epicgames.com/documentation/en-us/unreal-engine/BlueprintAPI/MotionWarping/AddorUpdateWarpTargetfromCompone- | Align vaults, takedowns, cover, vehicle entry to targets | High (UNVERIFIED maturity label) | Free | UE EULA | UE 5.x (5.8 BP API) | — | BP/Python | Follow moving targets | Docs page 403; maturity UNVERIFIED | — | Essential | Medium | BP API page (2026-10-09) |
| Mover plugin | Engine movement | Epic Games | https://dev.epicgames.com/documentation/unreal-engine/mover-in-unreal-engine | Next-gen networked movement | Experimental | Free | UE EULA | 5.4+ Experimental; Beta targeted 2026 | — | C++/BP | Rollback networking; GASP Mover chars | API churn; "use caution when shipping" | — | Optional | High | Epic docs; https://forums.unrealengine.com/t/question-about-the-next-steps-for-chaos-mover/2743117 (2026-10-09) |
| Unreal Animation Framework (UAF) | Engine anim framework | Epic Games | https://www.unrealengine.com/en-US/tech-blog/explore-the-updates-to-the-game-animation-sample-project-in-ue-5-7 | Future AnimBP replacement | Experimental | Free | UE EULA | 5.8 Experimental | — | UNVERIFIED | Future-proofing | Not production-ready | — | Avoid (for shipping now) | Medium | GASP 5.7 blog (2026-10-09) |
| MetaHuman Animator Markerless Motion Capture Plugin | Markerless video mocap (local) | Epic Games (Meshcapade tech) | https://www.metahuman.com/news/metahuman-5-8-is-now-available | Body + hands + face from one camera | Promising; floating feet reported; no metrics | Free | Free incl. commercial (Fab, per CG Channel) | UE 5.8+, Windows only, DX12 | UE anim sequences; export via UE | Python + BP automation | Free, local, no cloud; one camera | Experimental; slow (about 15–30 min per 30 s); static camera best | Webcam or phone; RTX 3070-class 8 GB VRAM, 32 GB RAM, 16 cores recommended | Essential | Medium-High | metahuman.com; https://www.cgchannel.com/2026/06/get-the-free-metahuman-animator-markerless-mocap-plugin/ ; https://digitalproduction.com/2026/06/23/metahuman-gets-markerless-mocap/ (2026-10-09) |
| Mixamo | Free anim library + auto-rigger | Adobe | https://www.mixamo.com/ | Prototype animations and auto-rig | Medium (dated) | Free (Adobe ID) | Royalty-free commercial; no raw redistribution; no ML training | Any (FBX); retarget via IK Retargeter | FBX | None | Free, large, auto-maps well | Unmaintained since 2015; outages; no support | Browser | Valuable (prototype) | Medium | https://community.adobe.com/questions-696/mixamo-faq-licensing-royalties-ownership-eula-and-tos-589400 ; https://app.cinevva.com/guides/free-character-animations-rigging (2026-10-09) |
| ActorCore | Paid mocap library | Reallusion | https://actorcore.reallusion.com/ | Mocap/hand-key motions, interactions, stunts, prop-paired | Medium-High (UNVERIFIED per pack) | Pay per content; prices UNVERIFIED | Reallusion Standard License (export, games OK); iContent not exportable | UE via FBX/USD; Auto Setup plugin | FBX, USD, iMotion | None found | Interactions/conversations; prop-paired motions | Prices not verified; library-style consistency varies | Browser | Valuable | Medium | https://www.reallusion.com/license/content.html ; https://actorcore.reallusion.com/3d-motion-technology?a= (2026-10-09) |
| AccuRIG 2 | Free auto-rigger | Reallusion | https://actorcore.reallusion.com/auto-rig | Rig static humanoids, preview ActorCore motions | Medium-High | Free | Free (ActorCore terms) | UE FBX preset | FBX, USD, iAvatar | None | Free, UE preset, A/T pose, multi-mesh | No skin-weight editing (needs CC) | Windows PC (UNVERIFIED macOS) | Valuable | Medium | https://manual.reallusion.com/AccuRig-2/2.0/09-add-motions/export.htm (2026-10-09) |
| iClone 8 | Animation suite + mocap hub | Reallusion | https://www.reallusion.com/plan-and-pricing/individual/perpetual | Editing, AI Video Mocap, Motion LIVE, UE Live Link | Medium-High | Perpetual or 365 subscription; prices UNVERIFIED | Reallusion EULA | UE Live Link; FBX | FBX, USD | UNVERIFIED (Python plugin system exists, unconfirmed) | Hub for Xsens/Rokoko/Vicon/OptiTrack | Extra ecosystem cost; iContent licence traps | Windows PC | Optional | Medium | Reallusion pricing pages (2026-10-09) |
| Cascadeur | AI-assisted animation and cleanup | Nekki | https://cascadeur.com/plans | Keyframe, AutoPosing, AutoPhysics, Inbetweening, Motion Generation, mocap cleanup | High (for cleanup/authoring) | Free; Indie $19/mo or $8/mo annual (<$100k revenue); Pro $49/mo or $33/mo annual; Teams $33/user/mo annual | Free non-commercial; Indie under revenue cap; Pro unlimited; annual gives perpetual builds | UE Live Link (paid; UE 5.5–5.7 per docs) | CASC, FBX, DAE, USD, GLTF, BVH import | Python API | Physics-plausible fixes; penetration cleaning; UE Live Link | Free tier can't export FBX; Live Link manual install | Win/macOS/Linux | Essential (Indie/Pro) | High | https://cascadeur.com/plans ; https://www.cgchannel.com/2026/04/nekki-releases-cascadeur-2026-1/ ; https://cascadeur.com/blog/view/cascadeur-2026-2-animation-layers-easing-new-languages (2026-10-09) |
| Rokoko Smartsuit Pro II / Smartgloves II / Coil Pro + Studio | Inertial mocap hardware + software | Rokoko | https://www.rokoko.com/pricing | Body/finger capture; live stream to UE | Medium-High (drift mitigated by Coil Pro) | Suit (glove-ready) $2,295; Coil Pro $2,995; Full bundle $4,495; Studio $0–$70/mo | Motion Library commercial OK; Studio output terms UNVERIFIED per tier | UE live plugin (Plus+) | FBX, BVH, CSV | Command API (Pro), UDP triggers | Repeatable, occlusion-free, solo-operable | Upfront cost; IMU drift without Coil | Suit + PC + Wi-Fi router | Valuable (after vertical slice) | High | https://www.rokoko.com/pricing ; https://store.rokoko.com/products/smartsuit-pro-ii-glove-ready ; https://www.rokoko.com/products/full-performance-capture (2026-10-09) |
| Rokoko Vision | Video AI mocap (cloud) | Rokoko | https://www.rokoko.com/pricing | Video-to-motion; text-to-motion clips | UNVERIFIED (no metrics) | 30 s/mo free; 600–15000 s/mo on $10–$50/mo annual plans | Commercial terms per tier UNVERIFIED | Via Studio export/stream | FBX, BVH | Command API (Pro) | Cheap entry; same Studio cleanup filters | Quality unbenchmarked | Webcam/phone | Optional | Medium | https://www.rokoko.com/pricing (2026-10-09) |
| Move AI (Move One / Pro / Live) | Markerless AI mocap (cloud/on-prem) | Move AI Ltd | https://www.move.ai/pricing | Single-camera (iPhone) to multi-cam (up to 8 people) | Medium-High (vendor claims; no metrics) | Pricing page: Free / $15 / $35 / $225 per month; docs: Personal $15 (non-commercial), Standard $50, Advanced $250, Pro trial $995 | Docs: Personal non-commercial; Standard+ commercial; current tiers UNVERIFIED | Move Live Unreal plug-in; FBX import | FBX, USD, MP4 | API (Enterprise / developers.move.ai) | Multi-person, large volume, 120 fps (Pro) | Official pages conflict; 60 s clip cap (One); credits | iPhone (One); multi-cam (Pro); 4–8 cams (Live) | Valuable (for multi-person cinematics) | Medium | https://www.move.ai/pricing ; https://docs.move.ai/knowledge/move-ai-pricing-plans-credits (2026-10-09) |
| DeepMotion Animate 3D / SayMotion | Video/text AI mocap (cloud) | DeepMotion | https://www.deepmotion.com/pricing-animate3d | Video-to-3D and text-to-3D motion | UNVERIFIED (no metrics) | Freemium 60 s/mo; paid tiers UNVERIFIED (third-party ~$15/mo Starter) | Freemium non-commercial; premium licensed | FBX into UE | FBX, BVH, GLB (UNVERIFIED) | API plans | Face/hand add-ons; text-to-motion | Prices unverified; physics-filter artefacts unbenchmarked | Browser | Optional | Medium | https://www.deepmotion.com/pricing-animate3d (2026-10-09) |
| Autodesk Flow Studio | Cloud AI mocap + VFX | Autodesk | https://www.autodesk.com/products/flow-studio/features | Markerless body/hand mocap, scene export | Medium-High (claims improved foot contact) | Free/Lite/Standard/Pro/Enterprise; 300/2100/6000/12000 credits; $ UNVERIFIED | Paid tier needed for project use; AI data opt-out default only on Enterprise (implies otherwise opt-in, UNVERIFIED) | UE scene export; FBX/USD | FBX, USD, Blender scene | UNVERIFIED | Auto foot locking; up to 7-person (Enterprise); RADiCAL tech incoming | Cloud data terms; prices not shown | Browser | Optional | Medium | https://www.autodesk.com/eu/products/flow-studio/compare ; https://www.autodesk.com/solutions/media-entertainment/ai-motion-capture-with-flow-studio (2026-10-09) |
| RADiCAL Motion | AI mocap (cloud) | RADiCAL (tech now Autodesk) | https://www.cgchannel.com/2026/04/autodesk-acquires-core-tech-of-ai-motion-capture-firm-radical/ | — | — | Discontinued | — | — | — | — | — | Portal closed 2026-07-06 | — | Avoid (dead) | High | CG Channel 2026-04-09 (2026-10-09) |
| Kinetix | AI animation | Kinetix (team now at Runway) | https://runway.com/news/company-news/kinetix-joins-runway | — | — | UNVERIFIED | UNVERIFIED | — | — | — | — | Team joined Runway 2026-09-08 (world models/robotics) | — | Avoid | Medium | Runway news (2026-10-09) |
| Plask Motion | Browser AI mocap | Plask (Seoul) | https://plask.ai/ | Video-to-motion + editor | UNVERIFIED | UNVERIFIED (secondary: free 15 s/day; Pro ~1 h/mo) | UNVERIFIED | Export to Unreal | FBX/BVH (UNVERIFIED) | UNVERIFIED | Browser-based, editor included | Small company; no recent funding news | Browser | Optional | Low | https://plask.ai/ ; https://startuply.vc/article/plask-s-browser-tab-replaces-the-motion-capture-studio-wu82qj (2026-10-09) |
| Uthana | AI text/video-to-motion, locomotion | Uthana | https://uthana.com/pricing | Generate/retarget motions; API | UNVERIFIED | Pay-as-you-go: $0.02–$0.10/s text; $0.05/s video; $0.02/s locomotion; $0.15/rig | UNVERIFIED | UE5 FBX preset | FBX, GLB, BVH | GraphQL API; Blender/Maya plugins | API-first (agent-friendly) | No new subscriptions since Aug 2026; 4–10 s clips (TTM 3.0) | Browser/API | Optional | Medium | https://uthana.com/pricing ; https://uthana.com/faq (2026-10-09) |
| Autodesk MotionMaker (Maya) | AI locomotion generator | Autodesk | https://blogs.autodesk.com/media-and-entertainment/2025/06/04/meet-motionmaker/ | Path/keyframe-driven locomotion | Medium-High (vendor) | Requires Maya (price UNVERIFIED) | Maya EULA | FBX from Maya | Maya → FBX | Maya scripting (UNVERIFIED) | Foot-slide reduction; BYO data training | Requires Maya | Workstation | Optional | Medium | Autodesk blog 2025-06-04 (2026-10-09) |
| Auto-Rig Pro | Blender rigging/retarget/export | Artell | https://superhivemarket.com/products/auto-rig-pro | Rig, Remap, UE Mannequin-compliant export | High (widely used: 69,900+ sales) | Full $50; Lite $25 | GPL add-on | UE4/UE5 Mannequin naming/axes/IK bones | FBX, GLTF | Python (arp_export) | Smart auto-rig; Mannequin export | Must match spine/neck counts; manual weight QA | Blender 2.93–5.2 | Essential | High | Superhive; https://www.lucky3d.fr/auto-rig-pro/doc/ge_export_doc.html (2026-10-09) |
| Rigify (+ Uefy) | Blender rigging | Blender Foundation (+ community Uefy) | https://sidney-eliot.github.io/3d-artists-handbook/presentation/blender-to-unreal-pipeline | Free rig with Manny metarig presets | Medium-High | Rigify free; Uefy lite free / pro UNVERIFIED | GPL (Rigify); Uefy UNVERIFIED | Via export + IK Retargeter | FBX | bpy | Free | More manual steps; script "breaks" rig for Blender use (route 2) | Blender | Valuable | Medium | 3D Artist's Handbook (2026-10-09) |
| Send to Unreal (BlenderTools fork) | Blender→UE bridge | Community fork of Epic BlenderTools | https://github.com/Tzeentchnet/BlenderTools | One-click mesh/anim transfer | Medium (validation in progress) | Free | UNVERIFIED (inherits Epic repo licence) | UE 5.7 target; legacy FBX importer needed on 5.5+ | FBX | Python remote execution | Agent-scriptable bridge | Not Epic-maintained; Blender 5 work in progress | — | Optional | Medium | GitHub fork (2026-10-09) |
| Quaternius Universal Animation Library 1 & 2 | Free anim library | Quaternius | https://quaternius.com/packs/universalanimationlibrary2.html | 250+ general humanoid anims (prototype/NPC) | Medium (stylised-generic) | Free standard; full source via site (price UNVERIFIED) | CC0 | UE 5+ (GLB/FBX); retarget | FBX, glTF, .blend | None | CC0, root-motion + in-place | Not photoreal-AAA mocap; small per-category coverage | — | Valuable (prototype/NPC) | High | Quaternius pages; itch.io (2026-10-09) |
| MoCap Online (Rifle Pro, Pistol Pro, etc.) | Paid mocap packs | MoCap Online | https://mocaponline.com/products/rifle | Armed locomotion, aim, reload, swaps | Medium-High (mocap, pose-matched) | UNVERIFIED | Fab Standard (UNVERIFIED per listing) | UE 4.14–5.6 listed; UE5 Manny via provided retargeters | UE project + FBX | None | 370–390+ anims per weapon; RM + IP | UE4 skeleton delivery; style consistency needs testing | — | Valuable | Medium | https://www.fab.com/listings/6cbfcfb5-e96b-4a69-b8f2-124844c17b8f (2026-10-09) |
| Kubold Cover Animset Pro | Paid mocap pack | Kubold | https://www.fab.com/listings/f9e9fbcb-0f07-49a0-8c06-80b18eba0e90 | Rifle cover system anims | Medium-High (mocap) | UNVERIFIED | Fab Standard (UNVERIFIED) | UE 4.8–5.6 listed | UE + FBX | None | Stand/crouch cover + 43 AO poses | Rifle only; no controller | — | Valuable | Medium | Fab listing (2026-10-09) |
| Silent Assassin Animation Bundle | Paid hand-keyed pack | UNVERIFIED (Fab seller) | https://www.fab.com/listings/b7f4345b-eadc-465c-8ad4-876c30322e39 | Stealth locomotion, takedowns, grabs, hostages | UNVERIFIED (seller claims "AAA-level") | UNVERIFIED | Fab Standard (UNVERIFIED) | UE5 Mannequin; UE 5.8 listed | UE + FBX | None | Direct espionage fit; victim reactions | Hand-keyed style may not match mocap | — | Valuable (evaluate) | Low-Medium | Fab listing (2026-10-09) |
| Big Locomotion Pack V05 | Paid mocap bundle | UNVERIFIED (Fab seller) | https://www.fab.com/listings/83060727-3112-4d31-af3a-f35f6236f518 | Unarmed/cover/rifle/pistol/shotgun/sniper locomotion | UNVERIFIED | UNVERIFIED | Fab Standard (UNVERIFIED) | UNVERIFIED | UE | None | 738 anims + 36 AO in one consistent style | Quality unverified | — | Valuable (evaluate) | Low-Medium | Fab listing (2026-10-09) |
| Cover System (Fab) | Paid anims + BP system | UNVERIFIED (Fab seller) | https://www.fab.com/listings/bb06916a-cb63-41c5-aad7-b54b5b01a320 | Cover mechanic + 242 anim files | UNVERIFIED | UNVERIFIED | Fab Standard (UNVERIFIED) | UE5 Mannequin, IK bones | UE + FBX + MotionBuilder | None | Includes detection/traversal logic | Not tested for multiplayer | — | Optional | Low-Medium | Fab listing (2026-10-09) |

### Inferences
- "Essential" rows form the minimum viable AAA-feel stack: UE built-ins, GASP, the MetaHuman markerless plugin, Cascadeur and Auto-Rig Pro.

### Gaps
- Many price cells are UNVERIFIED because vendor pages rendered prices client-side (Fab packs, ActorCore, DeepMotion, Flow Studio, iClone).

---

## 11. Unresolved questions

### Takeaway
The remaining unknowns are mostly prices, maturity labels, and real-world accuracy of AI mocap. None of them block the recommended free-first plan.

### Cited Findings
- None. This section lists open items only.

### Inferences
- These items should go to the cost/licensing specialist, or be verified hands-on in a vertical-slice test.

### Gaps
1. Did the 5.8 GASP ship the promised all-UAF character? The 5.7 blog promised it; the 5.8 blog is silent. — [5.7 blog](https://www.unrealengine.com/en-US/tech-blog/explore-the-updates-to-the-game-animation-sample-project-in-ue-5-7)
2. GASP per-category clip lists. Does it include crouch, strafe or pivot sets? The docs page returned 403.
3. Maturity labels in 5.8 for Motion Warping, Full Body IK, Pose Search/Motion Matching (Production-Ready or not), and Animation Deformers.
4. Exact current prices: Fab packs (MoCap Online, Kubold, Silent Assassin, Big Locomotion, Cover System), ActorCore packs, iClone 8, DeepMotion tiers, Flow Studio tiers, Plask.
5. Commercial rights on Move AI's current Starter and Basic tiers. The two official pages conflict.
6. Commercial terms for Rokoko Studio Starter-tier recordings and Rokoko Vision output; Uthana's output licence.
7. Independent accuracy benchmarks for the MetaHuman markerless plugin, Move AI, Rokoko Vision, DeepMotion and Flow Studio, especially foot contact, fast combat, ground work and prop contact.
8. Whether the MetaHuman markerless output can be used for non-MetaHuman custom characters without quality loss. Epic says it can "retarget to other character types", but this is not demonstrated in the sources.
9. Whether Mixamo has any official 2026 status statement from Adobe.
10. Availability and quality of vehicle enter/exit and ladder/climb packs on Fab. Not searched; overlaps with the vehicles specialist.
11. Whether Pose Search databases and Choosers can be fully generated by UE Python, for agent automation.
12. Whether Epic's original BlenderTools repo is archived.
